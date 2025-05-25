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

            var result = notes.Select(note => NoteDto.FromModel(note));

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<NoteDto>> GetNote(int id)
        {
            var sql = "SELECT * FROM Notes WHERE Id = @Id";

            using var conn = Connection;
            var note = await conn.QuerySingleOrDefaultAsync<Note>(sql, new { Id = id });

            if (note == null) return NotFound();

            var result = NoteDto.FromModel(note);

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<NoteDto>> CreateNote(NoteCreateDto dto)
        {
            var sql = @"
                INSERT INTO Notes (Title, Content, CreatedAt)
                OUTPUT INSERTED.*
                VALUES (@Title, @Content, CURRENT_TIMESTAMP);";

            using var conn = Connection;
            var newNote = await conn.QuerySingleAsync<Note>(sql, dto);

            var result = NoteDto.FromModel(newNote);

            return CreatedAtAction(nameof(GetNote), new { newNote.Id }, newNote);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteNote(int id)
        {
            var sql = "DELETE FROM Notes WHERE Id = @Id";

            using var conn = Connection;
            var rowsAffected = await conn.ExecuteAsync(sql, new { Id = id });

            if (rowsAffected == 0)
                return NotFound();

            return NoContent(); // 204 No Content
        }
    }
}
