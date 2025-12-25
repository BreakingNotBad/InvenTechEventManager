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
        public async Task<IActionResult> GetGuestUsers()
        {
            var items = await _service.Outsource.GetGuestUsersAsync();
            return Ok(items);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetGuestUserById(int id)
        {
            var item = await _service.Outsource.GetGuestUserByIdAsync(id);
            if (item == null) return NotFound();

            return Ok(item);
        }
    }
}
