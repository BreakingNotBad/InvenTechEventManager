using Entity.Domain.Model;
using Microsoft.AspNetCore.Mvc;
using Service.Contract;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RolesController : ControllerBase
    {
        private readonly IServiceManager _service;

        public RolesController(IServiceManager service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetRoles()
        {
            var items = await _service.Role.GetRolesAsync();
            return Ok(items);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetRoleById(int id)
        {
            var item = await _service.Role.GetRoleByIdAsync(id);
            if (item == null) return NotFound();

            return Ok(item);
        }
    }
}
