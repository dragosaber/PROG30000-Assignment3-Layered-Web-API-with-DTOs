using EventRegistrationAPI_Joseph_Thomas.DTOs;

namespace EventRegistrationAPI_Joseph_Thomas.Services
{
    public interface IEventService
    {
        Task<List<EventDto>> GetAllAsync();
        Task<EventDto?> GetByIdAsync(int id);
    }
}
