using EventRegistrationAPI_Joseph_Thomas.Data;
using EventRegistrationAPI_Joseph_Thomas.DTOs;
using EventRegistrationAPI_Joseph_Thomas.Exceptions;
using EventRegistrationAPI_Joseph_Thomas.Models;
using Microsoft.EntityFrameworkCore;

namespace EventRegistrationAPI_Joseph_Thomas.Services
{
    public class RegistrationService : IRegistrationService
    {
        private readonly AppDbContext _context;

        public RegistrationService(AppDbContext context)
        {
            _context = context;
        }

        // Returns all registrations for one event, or null if the event doesn't exist.
        public async Task<List<RegistrationDto>?> GetForEventAsync(int eventId)
        {
            if (!await _context.Events.AnyAsync(e => e.EventId == eventId))
            {
                return null;
            }

            return await _context.Registrations
                .Where(r => r.EventId == eventId)
                .OrderBy(r => r.RegisteredOn)
                .Select(r => new RegistrationDto
                {
                    RegistrationId = r.RegistrationId,
                    EventTitle = r.Event != null ? r.Event.Title : string.Empty,
                    StudentName = r.StudentName,
                    Email = r.Email,
                    RegisteredOn = r.RegisteredOn
                })
                .ToListAsync();
        }

        // Registers a person for an event.
        public async Task<RegistrationDto> RegisterAsync(int eventId, CreateRegistrationDto dto)
        {
            var ev = await _context.Events.FirstOrDefaultAsync(e => e.EventId == eventId);

            // Rule 1: the event must exist (404)
            if (ev == null)
            {
                throw new NotFoundException($"Event {eventId} was not found.");
            }

            // Rules 2 to 4, in order
            EnsureEventIsNotInThePast(ev);
            await EnsureEmailNotAlreadyRegisteredAsync(eventId, dto.Email);
            await EnsureEventHasSpaceAsync(ev);

            // Map DTO -> entity (the server sets RegisteredOn, the client never does)
            var registration = new Registration
            {
                EventId = eventId,
                StudentName = dto.StudentName.Trim(),
                Email = dto.Email.Trim(),
                RegisteredOn = DateTime.Now
            };

            _context.Registrations.Add(registration);
            await _context.SaveChangesAsync();

            // Read it back as a DTO so the response includes the event title
            return await _context.Registrations
                .Where(r => r.RegistrationId == registration.RegistrationId)
                .Select(r => new RegistrationDto
                {
                    RegistrationId = r.RegistrationId,
                    EventTitle = r.Event != null ? r.Event.Title : string.Empty,
                    StudentName = r.StudentName,
                    Email = r.Email,
                    RegisteredOn = r.RegisteredOn
                })
                .FirstAsync();
        }

        // Cancels a registration. Returns false if it doesn't exist.
        public async Task<bool> CancelAsync(int registrationId)
        {
            var registration = await _context.Registrations.FindAsync(registrationId);
            if (registration == null)
            {
                return false;
            }

            _context.Registrations.Remove(registration);
            await _context.SaveChangesAsync();
            return true;
        }

        // ---------- Business rules (one small method per rule) ----------

        // Rule 2: no registering once the event's date and time have passed (400)
        private static void EnsureEventIsNotInThePast(Event ev)
        {
            if (ev.EventDate <= DateTime.Now)
            {
                throw new BadRequestException("Registration is closed for past events.");
            }
        }

        // Rule 3: the same email can't register twice for one event (409).
        // Both sides are lower-cased so the comparison is case-insensitive.
        private async Task EnsureEmailNotAlreadyRegisteredAsync(int eventId, string email)
        {
            var cleanEmail = email.Trim().ToLower();

            if (await _context.Registrations.AnyAsync(r =>
                    r.EventId == eventId && r.Email.ToLower() == cleanEmail))
            {
                throw new ConflictException("This email is already registered for this event.");
            }
        }

        // Rule 4: the event can't have more registrations than its capacity (409)
        private async Task EnsureEventHasSpaceAsync(Event ev)
        {
            int registeredCount = await _context.Registrations.CountAsync(r => r.EventId == ev.EventId);

            if (registeredCount >= ev.Capacity)
            {
                throw new ConflictException("This event is full.");
            }
        }
    }
}