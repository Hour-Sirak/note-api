using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NoteApi.Models;
using System.Data;
using System.Data.SqlClient;

namespace NoteApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {

        private readonly IConfiguration _config;

        public NotesController(IConfiguration config)
        {
            _config = config;
        }

        private IDbConnection Connection => new SqlConnection(_config.GetConnectionString("DefaultConnection"));

        [HttpGet]
        public async Task<ActionResult<IEnumerable<NoteDto>>> GetNotes()
        {
            var sql = "SELECT * FROM Notes ORDER BY CreatedAt DESC";

            using var conn = Connection;
            var notes = await conn.QueryAsync<Note>(sql);

            var result = notes.Select(note => new NoteDto
            {
                Id = note.Id,
                Title = note.Title,
                Content = note.Content,
                CreatedAt = note.CreatedAt.ToString("yyyy-MM-dd"),
                UpdatedAt = note.UpdatedAt?.ToString("yyyy-MM-dd")
            });

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<NoteDto>> GetNote(int id)
        {
            var sql = "SELECT * FROM Notes WHERE Id = @Id";

            using var conn = Connection;
            var note = await conn.QuerySingleOrDefaultAsync<Note>(sql, new { Id = id });

            if (note == null) return NotFound();

            var dto = new NoteDto
            {
                Id = note.Id,
                Title = note.Title,
                Content = note.Content,
                CreatedAt = note.CreatedAt.ToString("yyyy-MM-dd"),
                UpdatedAt = note.UpdatedAt?.ToString("yyyy-MM-dd")
            };

            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult<NoteDto>> CreateNote(NoteCreateDto dto)
        {
            var sql = @"
                INSERT INTO Notes (Title, Content, CreatedAt)
                OUTPUT INSERTED.*
                VALUES (@Title, @Content, GETDATE());";

            using var conn = Connection;
            var newNote = await conn.QuerySingleAsync<NoteDto>(sql, dto);

            return CreatedAtAction(nameof(GetNote), new { newNote.Id }, newNote);
        }

    }
}
