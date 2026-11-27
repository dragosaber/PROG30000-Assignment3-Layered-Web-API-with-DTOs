using EventRegistrationAPI_Joseph_Thomas.DTOs;
using EventRegistrationAPI_Joseph_Thomas.Exceptions;
using EventRegistrationAPI_Joseph_Thomas.Services;
using Microsoft.AspNetCore.Mvc;

namespace EventRegistrationAPI_Joseph_Thomas.Controllers
{
    [Route("api")]
    [ApiController]
    public class RegistrationsController : ControllerBase
    {
        private readonly IRegistrationService _registrationService;

        public RegistrationsController(IRegistrationService registrationService)
        {
            _registrationService = registrationService;
        }

        // GET: api/events/5/registrations
        [HttpGet("events/{eventId}/registrations")]
        public async Task<ActionResult<IEnumerable<RegistrationDto>>> GetRegistrationsForEvent(int eventId)
        {
            var registrations = await _registrationService.GetForEventAsync(eventId);
            if (registrations == null)
            {
                return NotFound();    // 404 Not Found: the event doesn't exist
            }
            return Ok(registrations); // 200 OK (an empty list is still a 200)
        }

        // POST: api/events/5/registrations
        [HttpPost("events/{eventId}/registrations")]
        public async Task<ActionResult<RegistrationDto>> Register(int eventId, CreateRegistrationDto dto)
        {
            try
            {
                var created = await _registrationService.RegisterAsync(eventId, dto);
                return StatusCode(StatusCodes.Status201Created, created);   // 201 Created with the RegistrationDto
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);      // 404 Not Found: event doesn't exist
            }
            catch (BadRequestException ex)
            {
                return BadRequest(ex.Message);    // 400 Bad Request: event is in the past
            }
            catch (ConflictException ex)
            {
                return Conflict(ex.Message);      // 409 Conflict: duplicate email or event is full
            }
        }

        // DELETE: api/registrations/5
        [HttpDelete("registrations/{id}")]
        public async Task<IActionResult> CancelRegistration(int id)
        {
            var cancelled = await _registrationService.CancelAsync(id);
            if (!cancelled)
            {
                return NotFound();            // 404 Not Found
            }
            return NoContent();               // 204 No Content
        }
    }
}