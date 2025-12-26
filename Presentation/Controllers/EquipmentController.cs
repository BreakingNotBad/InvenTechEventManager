using Entity.Domain.Model;
using Microsoft.AspNetCore.Mvc;
using Service.Contract;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EquipmentController : ControllerBase
    {
        private readonly IServiceManager _service;

        public EquipmentController(IServiceManager service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetEquipment()
        {
            var items = await _service.Equipment.GetEquipmentAsync();
            return Ok(items);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetEquipmentById(int id)
        {
            var item = await _service.Equipment.GetEquipmentByIdAsync(id);
            if (item == null) return NotFound();

            return Ok(item);
        }
        [HttpPost]
        public async Task<IActionResult> CreateEquipment([FromBody] Equipments equipment)
        {
            await _service.Equipment.CreateEquipmentAsync(equipment);
            return CreatedAtAction(nameof(GetEquipmentById), new { id = equipment.EquipmentId }, equipment);
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteEquipment(int id)
        {
            await _service.Equipment.DeleteEquipment(id);
            return NoContent();
        }

    }
}
