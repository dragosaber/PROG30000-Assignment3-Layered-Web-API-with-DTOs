using EventRegistrationAPI_Joseph_Thomas.DTOs;
using EventRegistrationAPI_Joseph_Thomas.Services;
using Microsoft.AspNetCore.Mvc;

namespace EventRegistrationAPI_Joseph_Thomas.Controllers
{
    [ApiController]
    [Route("api")]
    public class RegistrationsController : ControllerBase
    { 
        private readonly IRegistrationService _service;
        public RegistrationsController(IRegistrationService service)
        {
            _service = service;
        }

        [HttpGet("events/{eventId}/registrations")]
        public async Task<ActionResult<List<RegistrationDto>>> GetForEvent(int eventId)
        {
            var list = await _service.GetForEventAsync(eventId);
            return list
        }

        [HttpPost("events/{eventId}/registrations")]
        public async Task<ActionResult<RegistrationDto>> Register(int eventId, CreateRegistrationDto dto)
        {
            try
            {
                var result = await _service.RegisterAsync(eventId, dto);
            }
        }

        [HttpDelete("registrations/{id}")]
        public async Task<IActionResult> Cancel(int id)
        { 
            await _service.CancelAsync(id) ? NoContent() : NotFound();
        }

    }
}
