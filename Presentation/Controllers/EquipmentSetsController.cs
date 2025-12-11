using Entity.Domain.Model;
using Microsoft.AspNetCore.Mvc;
using Service.Contract;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EquipmentSetsController : ControllerBase
    {
        private readonly IServiceManager _service;

        public EquipmentSetsController(IServiceManager service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetEquipmentSets()
        {
            var items = await _service.EquipmentSet.GetEquipmentSetsAsync();
            return Ok(items);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetEquipmentSetById(int id)
        {
            var item = await _service.EquipmentSet.GetEquipmentSetByIdAsync(id);
            if (item == null) return NotFound();

            return Ok(item);
        }
    }
}
