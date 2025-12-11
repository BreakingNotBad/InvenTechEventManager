using Entity.Domain.Model;
using Microsoft.AspNetCore.Mvc;
using Service.Contract;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventGuestsController : ControllerBase
    {
        private readonly IServiceManager _service;

        public EventGuestsController(IServiceManager service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetEventGuests()
        {
            var items = await _service.EventGuest.GetEventGuestsAsync();
            return Ok(items);
        }

        [HttpGet("by-event/{eventId:int}")]
        public async Task<IActionResult> GetEventGuestsByEvent(int eventId)
        {
            var items = await _service.EventGuest.GetByEventIdAsync(eventId);
            return Ok(items);
        }
    }
}
