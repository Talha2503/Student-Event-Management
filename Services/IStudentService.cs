using StudentEventManagement.DTOs;

namespace StudentEventManagement.Services
{
    public interface IStudentService
    {
        Task<StudentDto> CreateStudentAsync(CreateStudentDto dto);
        Task<IEnumerable<StudentDto>> GetAllStudentsAsync();
    }
}