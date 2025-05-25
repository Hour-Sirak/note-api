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

        public static NoteDto FromModel(Note note)
        {
            return new NoteDto
            {
                Id = note.Id,
                Title = note.Title,
                Content = note.Content,
                CreatedAt = note.CreatedAt.ToString("yyyy-MM-dd"),
                UpdatedAt = note.UpdatedAt?.ToString("yyyy-MM-dd")
            };    
        }
    }

    public class NoteCreateDto
    {
        [Required, MaxLength(255)]
        public required string Title { get; set; }

        public string? Content { get; set; }
    }
}
