using EventRegistrationAPI_Joseph_Thomas.DTOs;

namespace EventRegistrationAPI_Joseph_Thomas.Services
{
    public interface IRegistrationService
    {
        // Gets all registrations for a specific event, or null if the event doesn't exist.
        Task<List<RegistrationDto>?> GetForEventAsync(int eventId);
        // Registers a person for an event. Throws NotFoundException if the event doesn't exist, InvalidOperationException if the event is full, or ArgumentException if the email is already registered for that event.
        Task<RegistrationDto> RegisterAsync(int eventId, CreateRegistrationDto dto);
        // Cancels a registration. Returns false if it doesn't exist.
        Task<bool> CancelAsync(int registrationId);
    }
}
