using StudentEventManagement.DTOs;

namespace StudentEventManagement.Services
{
    public interface IFeedbackService
    {
        Task<FeedbackDto> SubmitFeedbackAsync(CreateFeedbackDto dto);
        Task<IEnumerable<FeedbackDto>> GetFeedbackByEventAsync(int eventId);
    }
}