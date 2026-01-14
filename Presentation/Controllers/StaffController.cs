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

            // 1. Manual Mapping: ย้ายข้อมูลจาก DTO ลง Entity
            // (เฉพาะข้อมูลพื้นฐาน ไม่รวม RoleIds)
            var staffEntity = new Staff
            {
                FullName = request.FullName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber
                // CreatedAt = DateTime.UtcNow // ถ้าไม่ได้ตั้งค่า Default ใน DB
            };

            // 2. เรียก Service: ส่ง Entity และ RoleIds ไปพร้อมกัน
            // (Service จะทำหน้าที่เอา RoleIds ไปยัดใส่ staffEntity.StaffRoles ให้เอง)
            await _service.Staff.CreateStaffAsync(staffEntity, request.RoleIds);

            // 3. Return ผลลัพธ์
            // ตอนนี้ staffEntity จะมี StaffId แล้ว เพราะผ่านการ SaveChanges ใน Service มาแล้ว
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
