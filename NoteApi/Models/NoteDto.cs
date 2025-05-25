using System.ComponentModel.DataAnnotations;

namespace NoteApi.Models
{
    public class NoteDto
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public string? Content { get; set; }
        public required string CreatedAt { get; set; }
        public string? UpdatedAt { get; set; }
    }

    public class NoteCreateDto
    {
        [Required, MaxLength(255)]
        public required string Title { get; set; }

        public string? Content { get; set; }
    }
}
