using Entity.Domain.Model;
using Microsoft.AspNetCore.Mvc;
using Service.Contract;

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
        public async Task<IActionResult> GetStaffMembers()
        {
            var staffList = await _service.Staff.GetStaffMembersAsync();
            return Ok(staffList);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetStaffById(int id)
        {
            var company = await _service.Company.GetCompanyByIdAsync(id);
            if (company == null) return NotFound();
            return Ok(company);
        }

        [HttpPost]
        public async Task<IActionResult> CreateStaff([FromBody] Staff staff)
        {
            await _service.Staff.CreateStaffAsync(staff);
            return CreatedAtAction(nameof(GetStaffById), new { id = staff.StaffId }, staff);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteStaff(int id)
        {
            await _service.Staff.DeleteStaffAsync(id);
            return NoContent();
        }
    }
}
