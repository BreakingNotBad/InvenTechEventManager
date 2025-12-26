using Entity.Domain.Model;
using Microsoft.AspNetCore.Mvc;
using Service.Contract;

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
        public async Task<IActionResult> GetOutsource()
        {
            var items = await _service.Outsource.GetOutsources();
            return Ok(items);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetOutsourceById(int id)
        {
            var item = await _service.Outsource.GetOutsourcesByIdAsync(id);
            if (item == null) return NotFound();

            return Ok(item);
        }
        [HttpGet("{active}")]
        public async Task<IActionResult> GetOutsourceActive(
            [FromQuery] string? search,
            [FromQuery] DateOnly? date,
            [FromQuery] string? time_period,
            [FromQuery] Boolean? filter_available
            )
        {
            var outsourceList = await _service.Outsource.GetOutsourceActiveAsync(
                search,
                date,
                time_period,
                filter_available
            );
            return Ok(outsourceList);
        }
        [HttpPost]
        public async Task<IActionResult> CreateOutsource([FromBody] Outsources outsource)
        {
            await _service.Outsource.CreateOutsourceAsync(outsource);
            return CreatedAtAction(nameof(GetOutsourceById), new { id = outsource.OutsourceId }, outsource);
        }
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateOutsource(int id, [FromBody] Outsources outsource)
        {
            await _service.Outsource.UpdateOutsourceAsync(id, outsource);
            return NoContent();
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteOutsource(int id)
        {
            await _service.Outsource.DeleteOutsource(id);
            return NoContent();
        }
    }
}
