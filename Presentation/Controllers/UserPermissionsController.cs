using Entity.Domain.Model;
using Microsoft.AspNetCore.Mvc;
using Service.Contract;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserPermissionsController : ControllerBase
    {
        private readonly IServiceManager _service;

        public UserPermissionsController(IServiceManager service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetUserPermissions()
        {
            var items = await _service.UserPermission.GetUserPermissionsAsync();
            return Ok(items);
        }

        [HttpGet("by-user/{userId:int}")]
        public async Task<IActionResult> GetUserPermissionsByUser(int userId)
        {
            var items = await _service.UserPermission.GetByUserIdAsync(userId);
            return Ok(items);
        }
    }
}
