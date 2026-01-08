using Entity.Domain.Model;
using Microsoft.AspNetCore.Mvc;
using Service.Contract.Manager;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StaffController : ControllerBase
    {
        private readonly IServiceManager _service;

        public StaffController(IServiceManager service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetStaffMembers(
            [FromQuery(Name = "q")] string? query,
            [FromQuery] string? status,
            [FromQuery] string? role,
            [FromQuery] bool? available,
            [FromQuery] DateTime? date,
            [FromQuery] string? period
        )
        {
            var staffList = await _service.Staff.GetStaffMembersAsync(
                query,
                status,
                role,
                available,
                date,
                period
            );

            return Ok(staffList);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetStaffById(int id)
        {
            var staff = await _service.Staff.GetStaffByIdAsync(id);
            return Ok(staff);
        }

        [HttpPost]
        public async Task<IActionResult> CreateStaff([FromBody] Staff staff)
        {
            await _service.Staff.CreateStaffAsync(staff);
            return CreatedAtAction(nameof(GetStaffById), new { id = staff.StaffId }, staff);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateStaff(int id, [FromBody] Staff staff)
        {
            await _service.Staff.UpdateStaffAsync(id, staff);
            return Ok(staff);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteStaff(int id)
        {
            await _service.Staff.DeleteStaffAsync(id);
            return NoContent();
        }
    }
}
