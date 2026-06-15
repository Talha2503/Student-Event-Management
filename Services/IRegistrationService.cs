using StudentEventManagement.DTOs;

namespace StudentEventManagement.Services
{
    public interface IRegistrationService
    {
        Task<RegistrationDto> RegisterStudentAsync(CreateRegistrationDto dto);
        Task<IEnumerable<RegistrationDto>> GetRegistrationsByEventAsync(int eventId);
    }
}