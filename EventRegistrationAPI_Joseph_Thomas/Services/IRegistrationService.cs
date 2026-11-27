using EventRegistrationAPI_Joseph_Thomas.DTOs;

namespace EventRegistrationAPI_Joseph_Thomas.Services
{
    public interface IRegistrationService
    {
        Task<List<RegistrationDto>?> GetForEventAsync(int eventId);   
        Task<RegistrationDto> RegisterAsync(int eventId, CreateRegistrationDto dto);
        Task<bool> CancelAsync(int registrationId);
    }
}
