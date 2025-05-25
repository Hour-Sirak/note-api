using System.ComponentModel.DataAnnotations;

namespace NoteApi.Models
{
    public class NoteDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string? Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

    public class NoteCreateDto
    {
        [Required, MaxLength(255)]
        public required string Title { get; set; }

        public string? Content { get; set; }
    }
}
