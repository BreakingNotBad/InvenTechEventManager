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
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteOutsource(int id)
        {
            await _service.Outsource.DeleteOutsource(id);
            return NoContent();
        }
    }
}
