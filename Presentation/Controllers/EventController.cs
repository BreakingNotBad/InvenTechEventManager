using Microsoft.AspNetCore.Mvc;
using Service.Contract;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventsController : ControllerBase
    {
        private readonly IServiceManager _service;
        public EventsController(IServiceManager service)
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
            if (ev == null) return NotFound();

            return Ok(ev);
        }
    }
}
