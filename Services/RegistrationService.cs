using Microsoft.EntityFrameworkCore;
using StudentEventManagement.Data;
using StudentEventManagement.DTOs;
using StudentEventManagement.Models;

namespace StudentEventManagement.Services
{
    public class RegistrationService : IRegistrationService
    {
        private readonly AppDbContext _context;

        public RegistrationService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<RegistrationDto> RegisterStudentAsync(CreateRegistrationDto dto)
        {
            var student = await _context.Students.FindAsync(dto.StudentId);
            if (student == null)
                throw new Exception("Student not found.");

            var ev = await _context.Events.FindAsync(dto.EventId);
            if (ev == null)
                throw new Exception("Event not found.");

            var duplicate = await _context.Registrations
                .AnyAsync(r => r.StudentId == dto.StudentId && r.EventId == dto.EventId);
            if (duplicate)
                throw new Exception("Student is already registered for this event.");

            var currentCount = await _context.Registrations
                .CountAsync(r => r.EventId == dto.EventId);
            if (currentCount >= ev.Capacity)
                throw new Exception("Event is fully booked.");

            var registration = new Registration
            {
                StudentId    = dto.StudentId,
                EventId      = dto.EventId,
                RegisteredAt = DateTime.UtcNow
            };

            _context.Registrations.Add(registration);
            await _context.SaveChangesAsync();

            return new RegistrationDto
            {
                Id           = registration.Id,
                StudentId    = registration.StudentId,
                EventId      = registration.EventId,
                RegisteredAt = registration.RegisteredAt,
                StudentName  = student.Name,
                EventTitle   = ev.Title
            };
        }

        public async Task<IEnumerable<RegistrationDto>> GetRegistrationsByEventAsync(int eventId)
        {
            return await _context.Registrations
                .Where(r => r.EventId == eventId)
                .Include(r => r.Student)
                .Include(r => r.Event)
                .Select(r => new RegistrationDto
                {
                    Id           = r.Id,
                    StudentId    = r.StudentId,
                    EventId      = r.EventId,
                    RegisteredAt = r.RegisteredAt,
                    StudentName  = r.Student.Name,
                    EventTitle   = r.Event.Title
                })
                .ToListAsync();
        }
    }
}