using Microsoft.AspNetCore.Mvc;
using StudentEventManagement.DTOs;
using StudentEventManagement.Services;

namespace StudentEventManagement.Controllers
{
    [ApiController]
    [Route("api/feedback")]
    public class FeedbackController : ControllerBase
    {
        private readonly IFeedbackService _feedbackService;

        public FeedbackController(IFeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
        }

        [HttpPost]
        public async Task<IActionResult> Submit([FromBody] CreateFeedbackDto dto)
        {
            try
            {
                var feedback = await _feedbackService.SubmitFeedbackAsync(dto);
                return CreatedAtAction(nameof(GetByEvent), new { eventId = feedback.EventId }, feedback);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("event/{eventId}")]
        public async Task<IActionResult> GetByEvent(int eventId)
        {
            var feedbacks = await _feedbackService.GetFeedbackByEventAsync(eventId);
            return Ok(feedbacks);
        }
    }
}