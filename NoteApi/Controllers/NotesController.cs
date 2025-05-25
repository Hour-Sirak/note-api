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
                CreatedAt = note.CreatedAt,
                UpdatedAt = note.UpdatedAt
            };

            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateNote(NoteCreateDto dto)
        {
            var sql = @"
            INSERT INTO Notes (Title, Content, CreatedAt)
            VALUES (@Title, @Content, GETDATE());
            SELECT CAST(SCOPE_IDENTITY() AS INT);";

            using var conn = Connection;
            var id = await conn.QuerySingleAsync<int>(sql, dto);

            return CreatedAtAction(nameof(GetNote), new { id }, id);
        }

    }
}
