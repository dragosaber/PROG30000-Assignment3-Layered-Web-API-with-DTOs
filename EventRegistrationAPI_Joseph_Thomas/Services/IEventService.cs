using EventRegistrationAPI_Joseph_Thomas.DTOs;

namespace EventRegistrationAPI_Joseph_Thomas.Services
{
    public interface IEventService
    {
        // Gets all events, including the number of registrations and spots left.
        Task<List<EventDto>> GetAllAsync();
        // Gets a single event by its ID, or null if it doesn't exist.
        Task<EventDto?> GetByIdAsync(int id);
        // Creates a new event. Throws ArgumentException if the category doesn't exist, or InvalidOperationException if the name is a duplicate.    
        Task<EventDto> CreateAsync(CreateEventDto dto);

    }
}
