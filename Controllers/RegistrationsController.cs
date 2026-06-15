using Microsoft.AspNetCore.Mvc;
using StudentEventManagement.DTOs;
using StudentEventManagement.Services;

namespace StudentEventManagement.Controllers
{
    [ApiController]
    [Route("api/registrations")]
    public class RegistrationsController : ControllerBase
    {
        private readonly IRegistrationService _registrationService;

        public RegistrationsController(IRegistrationService registrationService)
        {
            _registrationService = registrationService;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] CreateRegistrationDto dto)
        {
            try
            {
                var registration = await _registrationService.RegisterStudentAsync(dto);
                return CreatedAtAction(nameof(GetByEvent), new { eventId = registration.EventId }, registration);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("event/{eventId}")]
        public async Task<IActionResult> GetByEvent(int eventId)
        {
            var registrations = await _registrationService.GetRegistrationsByEventAsync(eventId);
            return Ok(registrations);
        }
    }
}