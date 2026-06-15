namespace StudentEventManagement.Models
{
    public class Feedback
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public int StudentId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; } = string.Empty;
        public DateTime SubmittedAt { get; set; } = DateTime.UtcNow;

        public Event Event { get; set; } = null!;
        public Student Student { get; set; } = null!;
    }
}