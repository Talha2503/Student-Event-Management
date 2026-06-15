using Microsoft.EntityFrameworkCore;
using StudentEventManagement.Data;
using StudentEventManagement.DTOs;
using StudentEventManagement.Models;

namespace StudentEventManagement.Services
{
    public class EventService : IEventService
    {
        private readonly AppDbContext _context;

        public EventService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<EventDto>> GetAllEventsAsync()
        {
            return await _context.Events
                .Where(e => e.EventDate >= DateTime.UtcNow)
                .Select(e => MapToDto(e))
                .ToListAsync();
        }

        public async Task<EventDto?> GetEventByIdAsync(int id)
        {
            var e = await _context.Events.FindAsync(id);
            return e == null ? null : MapToDto(e);
        }

        public async Task<EventDto> CreateEventAsync(CreateEventDto dto)
        {
            var ev = new Event
            {
                Title       = dto.Title,
                Description = dto.Description,
                Venue       = dto.Venue,
                EventDate   = dto.EventDate,
                Capacity    = dto.Capacity
            };
            _context.Events.Add(ev);
            await _context.SaveChangesAsync();
            return MapToDto(ev);
        }

        public async Task<EventDto?> UpdateEventAsync(int id, CreateEventDto dto)
        {
            var ev = await _context.Events.FindAsync(id);
            if (ev == null) return null;

            ev.Title       = dto.Title;
            ev.Description = dto.Description;
            ev.Venue       = dto.Venue;
            ev.EventDate   = dto.EventDate;
            ev.Capacity    = dto.Capacity;

            await _context.SaveChangesAsync();
            return MapToDto(ev);
        }

        public async Task<bool> DeleteEventAsync(int id)
        {
            var ev = await _context.Events.FindAsync(id);
            if (ev == null) return false;

            _context.Events.Remove(ev);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<EventDto>> SearchEventsAsync(string query)
        {
            return await _context.Events
                .Where(e => e.Title.Contains(query) || e.Venue.Contains(query))
                .Select(e => MapToDto(e))
                .ToListAsync();
        }

        public async Task<IEnumerable<EventDto>> FilterEventsByDateAsync(string sort)
        {
            var events = _context.Events.AsQueryable();

            events = sort == "desc"
                ? events.OrderByDescending(e => e.EventDate)
                : events.OrderBy(e => e.EventDate);

            return await events.Select(e => MapToDto(e)).ToListAsync();
        }

        private static EventDto MapToDto(Event e) => new EventDto
        {
            Id          = e.Id,
            Title       = e.Title,
            Description = e.Description,
            Venue       = e.Venue,
            EventDate   = e.EventDate,
            Capacity    = e.Capacity
        };
    }
}