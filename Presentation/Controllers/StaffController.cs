using Contract.Interfaces.DTOs;
using Entity.Domain.Model;
using Microsoft.AspNetCore.Mvc;
using Presentation.Requests.Staff;
using Presentation.Requests.StaffRequests;
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
        public async Task<IActionResult> CreateStaff([FromForm] CreateStaffRequest request)
        {
            // --- Step 1: แปลง CreateStaffRequest (ที่มีไฟล์) ให้เป็น CreateStaffDto (ไม่มีไฟล์) ---
            // เราต้อง "สร้างใหม่" (new) ขึ้นมาเอง แล้วจับยัดข้อมูลใส่ทีละตัวครับ
            var staffDto = new CreateStaffDto
            {
                FullName = request.FullName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                RoleIds = request.RoleIds
            };

            // --- Step 2: เตรียม Stream จากไฟล์ (ถ้ามี) ---
            Stream? stream = null;
            string? fileName = null;

            if (request.AvatarFile != null)
            {
                stream = request.AvatarFile.OpenReadStream();
                fileName = request.AvatarFile.FileName;
            }

            // --- Step 3: ส่งของ 3 ชิ้นให้ Service (Dto, Stream, FileName) ---
            // ใช้ using เพื่อปิด Stream อัตโนมัติเมื่อ Service ทำงานจบ
            using (stream)
            {
                // ตรงนี้แหละครับ คือการส่งของให้ Service
                // Service จะคืนค่ากลับมาเป็น 'Staff' entity (result)
                var resultStaff = await _service.Staff.CreateStaffAsync(staffDto, stream, fileName);

                // --- Step 4: Return ผลลัพธ์กลับไปให้ Client ---
                // เอา resultStaff ที่ได้จาก Service ส่งกลับไป
                return CreatedAtAction(
                    nameof(GetStaffById),
                    new { id = resultStaff.StaffId },
                    resultStaff
                );
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateStaff(
            int id,
            [FromForm] UpdateStaffRequest request)
        {
            var dto = new UpdateStaffDto
            {
                
                FullName = request.FullName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                RoleIds = request.RoleIds,
                RemoveAvatar = request.RemoveAvatar,
                IsDeleted = request.IsDeleted
            };
            Stream? stream = null;
            string? fileName = null;

            if (request.AvatarFile != null)
            {
                stream = request.AvatarFile.OpenReadStream();
                fileName = request.AvatarFile.FileName;
            }


            using (stream)
            {
                await _service.Staff.UpdateStaffAsync(id, dto,stream, fileName);
            }

            return NoContent();
        }

        [HttpPatch("{id:int}")]
        public async Task<IActionResult> SoftDeleteStaff(int id, bool isDeleted)
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
