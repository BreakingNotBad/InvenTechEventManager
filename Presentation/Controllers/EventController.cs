using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts.DTOs.Event;
using Service.Contracts.Manager;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/events")]
    public class EventController : ControllerBase
    {
        private readonly IServiceManager _service;

        public EventController(IServiceManager service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetEvents()
        {
            var eventsList = await _service.Event.GetEventsAsync();
            return Ok(eventsList);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetEventById(int id)
        {
            var ev = await _service.Event.GetEventByIdAsync(id);
            if (ev == null)
                return NotFound();

            return Ok(ev);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEvent([FromBody] CreateEventDto eventDto)
        {
            await _service.Event.CreateEventAsync(eventDto);
            return CreatedAtAction(nameof(GetEventById), new { id = eventDto.EventName }, eventDto);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            await _service.Event.DeleteEvent(id);
            return NoContent();
        }
    }
}
