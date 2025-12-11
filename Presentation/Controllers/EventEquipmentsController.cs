using Entity.Domain.Model;
using Microsoft.AspNetCore.Mvc;
using Service.Contract;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventEquipmentsController : ControllerBase
    {
        private readonly IServiceManager _service;

        public EventEquipmentsController(IServiceManager service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetEventEquipments()
        {
            var items = await _service.EventEquipment.GetEventEquipmentsAsync();
            return Ok(items);
        }

        [HttpGet("by-event/{eventId:int}")]
        public async Task<IActionResult> GetEventEquipmentsByEvent(int eventId)
        {
            var items = await _service.EventEquipment.GetByEventIdAsync(eventId);
            return Ok(items);
        }
    }
}
