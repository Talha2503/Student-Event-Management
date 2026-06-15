namespace StudentEventManagement.DTOs
{
    public class FeedbackDto
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public int StudentId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; } = string.Empty;
        public DateTime SubmittedAt { get; set; }
    }

    public class CreateFeedbackDto
    {
        public int EventId { get; set; }
        public int StudentId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; } = string.Empty;
    }
}