using StudentEventManagement.DTOs;

namespace StudentEventManagement.Services
{
    public interface IEventService
    {
        Task<IEnumerable<EventDto>> GetAllEventsAsync();
        Task<EventDto?> GetEventByIdAsync(int id);
        Task<EventDto> CreateEventAsync(CreateEventDto dto);
        Task<EventDto?> UpdateEventAsync(int id, CreateEventDto dto);
        Task<bool> DeleteEventAsync(int id);
        Task<IEnumerable<EventDto>> SearchEventsAsync(string query);
        Task<IEnumerable<EventDto>> FilterEventsByDateAsync(string sort);
    }
}