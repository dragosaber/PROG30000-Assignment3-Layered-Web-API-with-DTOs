using EventRegistrationAPI_Joseph_Thomas.DTOs;
using EventRegistrationAPI_Joseph_Thomas.Services;
using Microsoft.AspNetCore.Mvc;

namespace EventRegistrationAPI_Joseph_Thomas.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventsController : ControllerBase
    {
        private readonly IEventService _eventService;
        public EventsController(IEventService eventService)
        {
            _eventService = eventService;
        }

        // GET: api/events
        [HttpGet]
        public async Task<ActionResult<List<EventDto>>> GetAll()
        {
            var products = await _eventService.GetAllAsync();
            return Ok(products);
        }

        // GET: api/events/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EventDto>> GetById(int id)
        {
            var product = await _eventService.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        // POST: api/events
        [HttpPost]
        public async Task<ActionResult<EventDto>> CreateEvent(CreateEventDto dto)
        {
            try
            {
                var created = await _eventService.CreateAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = created.EventId }, created);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);    // 400 Bad Request: category doesn't exist
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);      // 409 Conflict: duplicate name
            }
        }
    }
}
