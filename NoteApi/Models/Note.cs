namespace NoteApi.Models
{
    public class Note
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public string? Content { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
