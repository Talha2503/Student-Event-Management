using Microsoft.AspNetCore.Mvc;
using StudentEventManagement.DTOs;
using StudentEventManagement.Services;

namespace StudentEventManagement.Controllers
{
    [ApiController]
    [Route("api/events")]
    public class EventsController : ControllerBase
    {
        private readonly IEventService _eventService;

        public EventsController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var events = await _eventService.GetAllEventsAsync();
            return Ok(events);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var ev = await _eventService.GetEventByIdAsync(id);
            if (ev == null) return NotFound(new { message = "Event not found." });
            return Ok(ev);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateEventDto dto)
        {
            var ev = await _eventService.CreateEventAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = ev.Id }, ev);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CreateEventDto dto)
        {
            var ev = await _eventService.UpdateEventAsync(id, dto);
            if (ev == null) return NotFound(new { message = "Event not found." });
            return Ok(ev);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _eventService.DeleteEventAsync(id);
            if (!result) return NotFound(new { message = "Event not found." });
            return Ok(new { message = "Event deleted successfully." });
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string query)
        {
            if (string.IsNullOrWhiteSpace(query))
                return BadRequest(new { message = "Search query cannot be empty." });
            var events = await _eventService.SearchEventsAsync(query);
            return Ok(events);
        }

        [HttpGet("filter")]
        public async Task<IActionResult> Filter([FromQuery] string sort = "asc")
        {
            var events = await _eventService.FilterEventsByDateAsync(sort);
            return Ok(events);
        }
    }
}