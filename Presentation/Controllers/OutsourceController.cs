using Entity.Domain.Model;
using Microsoft.AspNetCore.Mvc;
using Service.Contract.Manager;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OutsourceController : ControllerBase
    {
        private readonly IServiceManager _service;

        public OutsourceController(IServiceManager service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetOutsources()
        {
            var items = await _service.Outsource.GetOutsources();
            return Ok(items);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetOutsourceById(int id)
        {
            var item = await _service.Outsource.GetOutsourcesByIdAsync(id);
            if (item == null)
                return NotFound();

            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOutsource([FromBody] Outsource outsource)
        {
            await _service.Outsource.CreateOutsourceAsync(outsource);
            return CreatedAtAction(
                nameof(GetOutsourceById),
                new { id = outsource.OutsourceId },
                outsource
            );
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateOutsource(int id, [FromBody] Outsource outsource)
        {
            await _service.Outsource.UpdateOutsourceAsync(id, outsource);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteOutsource(int id)
        {
            await _service.Outsource.DeleteOutsourceAsync(id);
            return NoContent();
        }
    }
}
