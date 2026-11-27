using EventRegistrationAPI_Joseph_Thomas.Data;

namespace EventRegistrationAPI_Joseph_Thomas.Services
{
    public class RegistrationService
    {
        private readonly AppDbContext _context;

        public RegistrationService(AppDbContext context)
        {
            _context = context;
        }

    }
}
