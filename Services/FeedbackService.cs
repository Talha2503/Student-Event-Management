using Microsoft.EntityFrameworkCore;
using StudentEventManagement.Data;
using StudentEventManagement.DTOs;
using StudentEventManagement.Models;

namespace StudentEventManagement.Services
{
    public class FeedbackService : IFeedbackService
    {
        private readonly AppDbContext _context;

        public FeedbackService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<FeedbackDto> SubmitFeedbackAsync(CreateFeedbackDto dto)
        {
            var ev = await _context.Events.FindAsync(dto.EventId);
            if (ev == null)
                throw new Exception("Event not found.");

            if (ev.EventDate > DateTime.UtcNow)
                throw new Exception("Feedback can only be submitted after the event date.");

            if (dto.Rating < 1 || dto.Rating > 5)
                throw new Exception("Rating must be between 1 and 5.");

            var student = await _context.Students.FindAsync(dto.StudentId);
            if (student == null)
                throw new Exception("Student not found.");

            var feedback = new Feedback
            {
                EventId     = dto.EventId,
                StudentId   = dto.StudentId,
                Rating      = dto.Rating,
                Comment     = dto.Comment,
                SubmittedAt = DateTime.UtcNow
            };

            _context.Feedbacks.Add(feedback);
            await _context.SaveChangesAsync();

            return new FeedbackDto
            {
                Id          = feedback.Id,
                EventId     = feedback.EventId,
                StudentId   = feedback.StudentId,
                Rating      = feedback.Rating,
                Comment     = feedback.Comment,
                SubmittedAt = feedback.SubmittedAt
            };
        }

        public async Task<IEnumerable<FeedbackDto>> GetFeedbackByEventAsync(int eventId)
        {
            return await _context.Feedbacks
                .Where(f => f.EventId == eventId)
                .Select(f => new FeedbackDto
                {
                    Id          = f.Id,
                    EventId     = f.EventId,
                    StudentId   = f.StudentId,
                    Rating      = f.Rating,
                    Comment     = f.Comment,
                    SubmittedAt = f.SubmittedAt
                })
                .ToListAsync();
        }
    }
}