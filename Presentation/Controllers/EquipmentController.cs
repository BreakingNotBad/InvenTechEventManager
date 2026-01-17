using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts.Manager;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/equipments")]
    public class EquipmentController : ControllerBase
    {
        private readonly IServiceManager _service;

        public EquipmentController(IServiceManager service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetEquipment(string? equipmentName, string? category)
        {
            var items = await _service.Equipment.GetEquipmentAsync(equipmentName, category);
            return Ok(items);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetEquipmentById(int id)
        {
            var item = await _service.Equipment.GetEquipmentByIdAsync(id);
            if (item == null)
                return NotFound();

            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEquipment([FromBody] Equipment equipment)
        {
            await _service.Equipment.CreateEquipmentAsync(equipment);
            return CreatedAtAction(
                nameof(GetEquipmentById),
                new { id = equipment.EquipmentId },
                equipment
            );
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateEquipment(int id, [FromBody] Equipment equipment)
        {
            await _service.Equipment.UpdateEquipmentAsync(id, equipment);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteEquipment(int id)
        {
            await _service.Equipment.DeleteEquipment(id);
            return NoContent();
        }
    }
}
