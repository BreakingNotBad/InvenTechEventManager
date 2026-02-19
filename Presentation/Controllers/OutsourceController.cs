using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts.DTOs.Outsource;
using Service.Contracts.Manager;
using Shared.RequestFeatures.Parameters;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/outsources")]
    public class OutsourceController : ControllerBase
    {
        private readonly IServiceManager _service;

        public OutsourceController(IServiceManager service)
        {
            _service = service;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetOutsources([FromQuery ]OutsourceParameter outsourceParameter)
        {
            var items = await _service.Outsource.GetOutsources(outsourceParameter);
            return Ok(items);
        }

        [HttpGet("{id:int}")]
        [Authorize]
        public async Task<IActionResult> GetOutsourceById(int id)
        {
            var item = await _service.Outsource.GetOutsourcesByIdAsync(id);
            if (item == null)
                return NotFound();

            return Ok(item);
        }

        [HttpPost]
        [Authorize(Policy = "admin")]
        public async Task<IActionResult> CreateOutsource([FromBody] CreateOutsourceDto dto)
        {
            var createdOutsource = await _service.Outsource.CreateOutsourceAsync(dto);

            return CreatedAtAction(
                nameof(GetOutsourceById),
                new { id = createdOutsource.OutsourceId },
                createdOutsource
            );
        }

        [HttpPut("{id:int}")]
        [Authorize(Policy = "admin")]
        public async Task<IActionResult> UpdateOutsource(int id, [FromBody] UpdateOutsourceDto dto)
        {
            await _service.Outsource.UpdateOutsourceAsync(id, dto);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        [Authorize(Policy = "admin")]
        public async Task<IActionResult> DeleteOutsource(int id)
        {
            await _service.Outsource.DeleteOutsourceAsync(id);
            return NoContent();
        }
    }
}
