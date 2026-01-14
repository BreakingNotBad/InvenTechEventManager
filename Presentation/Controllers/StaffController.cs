using Contract.Interfaces.DTOs;
using Entity.Domain.Model;
using Microsoft.AspNetCore.Mvc;
using Service.Contract.Manager;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/staff")]
    public class StaffController : ControllerBase
    {
        private readonly IServiceManager _service;

        public StaffController(IServiceManager service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetStaffMembers(
            string? fullName,
            string? status,
            string? role,
            DateTime? date,
            bool? available,
            string? period
        )
        {
            var staffList = await _service.Staff.GetStaffMembersAsync(
                fullName,
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
        public async Task<IActionResult> CreateStaff([FromBody] CreateStaffRequest request)
        {
            if (request == null)
            {
                return BadRequest("CreateStaffRequest object is null");
            }

            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }

        
            var staffEntity = new Staff
            {
                FullName = request.FullName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber
              
            };

            
            await _service.Staff.CreateStaffAsync(staffEntity, request.RoleIds);

       
            return CreatedAtAction(nameof(GetStaffById), new { id = staffEntity.StaffId }, staffEntity);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateStaff(int id,Staff staff)
        {
            await _service.Staff.UpdateStaffAsync(id, staff);
            return Ok(staff);
        }
        [HttpPatch("{id:int}")]
        public async Task<IActionResult> SoftDeleteStaff(int id,bool isDeleted)
        {
            await _service.Staff.SoftDeleteStaffAsync(id, isDeleted);
            return Ok(new { status = 200 });
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteStaff(int id)
        {
            await _service.Staff.DeleteStaffAsync(id);
            return NoContent();
        }
    }
}
