using Entity.Domain.Model;
using Microsoft.AspNetCore.Mvc;
using Service.Contract;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventStaffController : ControllerBase
    {
        private readonly IServiceManager _service;

        public EventStaffController(IServiceManager service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetEventStaffMembers()
        {
            var staff = await _service.EventStaff.GetEventStaffMembersAsync();
            return Ok(staff);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetEventStaffById(int id)
        {
            var staff = await _service.EventStaff.GetEventStaffByIdAsync(id);
            if (staff == null) return NotFound();

            return Ok(staff);
        }
    }
}
