using Microsoft.EntityFrameworkCore;
using StudentEventManagement.Data;
using StudentEventManagement.DTOs;
using StudentEventManagement.Models;

namespace StudentEventManagement.Services
{
    public class StudentService : IStudentService
    {
        private readonly AppDbContext _context;

        public StudentService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<StudentDto> CreateStudentAsync(CreateStudentDto dto)
        {
            var exists = await _context.Students
                .AnyAsync(s => s.Email == dto.Email);
            if (exists)
                throw new Exception("A student with this email already exists.");

            var student = new Student
            {
                Name          = dto.Name,
                Email         = dto.Email,
                StudentNumber = dto.StudentNumber
            };

            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            return new StudentDto
            {
                Id            = student.Id,
                Name          = student.Name,
                Email         = student.Email,
                StudentNumber = student.StudentNumber
            };
        }

        public async Task<IEnumerable<StudentDto>> GetAllStudentsAsync()
        {
            return await _context.Students
                .Select(s => new StudentDto
                {
                    Id            = s.Id,
                    Name          = s.Name,
                    Email         = s.Email,
                    StudentNumber = s.StudentNumber
                })
                .ToListAsync();
        }
    }
}