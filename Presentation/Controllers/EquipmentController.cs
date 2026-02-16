using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts.DTOs.Equipment;
using Service.Contracts.Manager;
using Shared.RequestFeatures.Parameters;

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
        public async Task<IActionResult> GetEquipment(
            [FromQuery] EquipmentParameter equipmentParameter)
        {
            var items = await _service.Equipment.GetEquipmentAsync(equipmentParameter);
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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateEquipment(CreateEquipmentDto dto)
        {
            var createEquipment = await _service.Equipment.CreateEquipmentAsync(dto);

            return CreatedAtAction(
                nameof(GetEquipmentById),
                new { id = createEquipment.EquipmentId },
                createEquipment
            );
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateEquipment(int id, UpdateEquipmentDto dto)
        {
            await _service.Equipment.UpdateEquipmentAsync(id, dto);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteEquipment(int id)
        {
            await _service.Equipment.DeleteEquipment(id);
            return NoContent();
        }
    }
}
