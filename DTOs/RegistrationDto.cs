namespace StudentEventManagement.DTOs
{
    public class RegistrationDto
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int EventId { get; set; }
        public DateTime RegisteredAt { get; set; }
        public string StudentName { get; set; } = string.Empty;
        public string EventTitle { get; set; } = string.Empty;
    }

    public class CreateRegistrationDto
    {
        public int StudentId { get; set; }
        public int EventId { get; set; }
    }
}