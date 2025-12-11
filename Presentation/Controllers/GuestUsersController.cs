using Entity.Domain.Model;
using Microsoft.AspNetCore.Mvc;
using Service.Contract;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GuestUsersController : ControllerBase
    {
        private readonly IServiceManager _service;

        public GuestUsersController(IServiceManager service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetGuestUsers()
        {
            var items = await _service.GuestUser.GetGuestUsersAsync();
            return Ok(items);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetGuestUserById(int id)
        {
            var item = await _service.GuestUser.GetGuestUserByIdAsync(id);
            if (item == null) return NotFound();

            return Ok(item);
        }
    }
}
