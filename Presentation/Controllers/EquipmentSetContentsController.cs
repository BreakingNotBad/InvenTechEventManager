using Entity.Domain.Model;
using Microsoft.AspNetCore.Mvc;
using Service.Contract;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EquipmentSetContentsController : ControllerBase
    {
        private readonly IServiceManager _service;

        public EquipmentSetContentsController(IServiceManager service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetEquipmentSetContents()
        {
            var contents = await _service.EquipmentSetContent.GetEquipmentSetContentsAsync();
            return Ok(contents);
        }

        [HttpGet("by-set/{equipmentSetId:int}")]
        public async Task<IActionResult> GetContentsByEquipmentSet(int equipmentSetId)
        {
            var contents = await _service.EquipmentSetContent.GetContentsByEquipmentSetIdAsync(equipmentSetId);
            return Ok(contents);
        }
    }
}
