using Entity.Domain.Model;
using Microsoft.AspNetCore.Mvc;
using Service.Contract;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PermissionsController : ControllerBase
    {
        private readonly IServiceManager _service;

        public PermissionsController(IServiceManager service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetPermissions()
        {
            var items = await _service.Permission.GetPermissionsAsync();
            return Ok(items);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetPermissionById(int id)
        {
            var item = await _service.Permission.GetPermissionByIdAsync(id);
            if (item == null) return NotFound();

            return Ok(item);
        }
    }
}
