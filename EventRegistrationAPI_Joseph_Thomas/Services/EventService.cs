using Microsoft.EntityFrameworkCore;
using EventRegistrationAPI_Joseph_Thomas.Data;
using EventRegistrationAPI_Joseph_Thomas.DTOs;
using EventRegistrationAPI_Joseph_Thomas.Models;

namespace EventRegistrationAPI_Joseph_Thomas.Services
{
    public class EventService : IEventService
    {
        private readonly AppDbContext _context;
        public EventService(AppDbContext context) { _context = context; }

        public async Task<List<EventDto>> GetAllAsync()
        {
            return await _context.Events
                .Select(e => new EventDto
                {
                    EventId = e.EventId,
                    Title = e.Title,
                    Location = e.Location,
                    EventDate = e.EventDate,
                    Capacity = e.Capacity,
                    RegisteredCount = e.Registrations.Count,
                    SpotsLeft = e.Capacity - e.Registrations.Count
                })
                .ToListAsync();
        }

        public async Task<EventDto?> GetByIdAsync(int id)
        {
            return await _context.Events
               .Where(e => e.EventId == id)
               .Select(e => new EventDto
               {
                   EventId = e.EventId,
                   Title = e.Title,
                   Location = e.Location,
                   EventDate = e.EventDate,
                   Capacity = e.Capacity,
                   RegisteredCount = e.Registrations.Count,
                   SpotsLeft = e.Capacity - e.Registrations.Count
               })
               .FirstOrDefaultAsync();
        }

    }
}
