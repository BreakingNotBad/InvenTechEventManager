using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Presentation.Requests.Staff;
using Service.Contracts.DTOs.Staff;
using Service.Contracts.IService;
using Service.Contracts.Manager;
using Shared.RequestFeatures.Parameters;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/staff")]
    public class StaffController : ControllerBase
    {
        private readonly IServiceManager _service;
        private readonly IFileService _fileService;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateStaffRequest> _createValidator;
        private readonly IValidator<UpdateStaffRequest> _updateValidator;

        public StaffController(
            IServiceManager service,
            IFileService fileService,
            IMapper mapper,
            IValidator<CreateStaffRequest> createValidator,
            IValidator<UpdateStaffRequest> updateValidator
        )
        {
            _service = service;
            _fileService = fileService;
            _mapper = mapper;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        [HttpGet]
        public async Task<IActionResult> GetStaffMembers(
            [FromQuery] StaffParameter staffParameter
        )
        {
            var staffList = await _service.Staff.GetStaffMembersAsync(staffParameter
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
        public async Task<IActionResult> CreateStaff([FromForm] CreateStaffRequest staffRequest)
        {
            // เรียก Validator (เช็คไฟล์)
            await _createValidator.ValidateAndThrowAsync(staffRequest);

            string? savedAvatar = null;

            // ถ้ามีไฟล์แนบมา ให้ Upload ที่นี่เลย
            if (staffRequest.AvatarFile != null && staffRequest.AvatarFile.Length > 0)
            {
                // ใช้ using เพื่อให้มันปิด Stream อัตโนมัติเมื่อใช้เสร็จ (กันไฟล์ค้าง/กันลืมปิด)
                using var stream = staffRequest.AvatarFile.OpenReadStream();

                // สั่ง FileService ให้ Save ลง Disk
                savedAvatar = await _fileService.SaveFileAsync(
                    stream,
                    staffRequest.AvatarFile.FileName,
                    "Staff"
                );
            }

            // แปลงข้อมูลจาก Request เป็น Dto
            var staffDto = _mapper.Map<CreateStaffDto>(staffRequest);
            staffDto.Avatar = savedAvatar;

            var createdStaff = await _service.Staff.CreateStaffAsync(staffDto);

            return CreatedAtAction(
                nameof(GetStaffById),
                new { id = createdStaff.StaffId },
                createdStaff
            );
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateStaff(
            int id,
            [FromForm] UpdateStaffRequest staffRequest
        )
        {
            await _updateValidator.ValidateAndThrowAsync(staffRequest);

            var staffDto = _mapper.Map<UpdateStaffDto>(staffRequest);

            // ถ้ามีไฟล์ใหม่แนบมา ให้ Upload ที่นี่เลย
            if (staffRequest.AvatarFile != null && staffRequest.AvatarFile.Length > 0)
            {
                // ใช้ using เพื่อให้มันปิด Stream อัตโนมัติเมื่อใช้เสร็จ (กันไฟล์ค้าง/กันลืมปิด)
                using var stream = staffRequest.AvatarFile.OpenReadStream();

                // ส่งให้ FileService บันทึก แล้วเอา Path กลับมา
                string newAvatar = await _fileService.SaveFileAsync(
                    stream,
                    staffRequest.AvatarFile.FileName,
                    "Staff"
                );

                // ยัด Path ใหม่ใส่ DTO
                staffDto.Avatar = newAvatar;
            }

            await _service.Staff.UpdateStaffAsync(id, staffDto);

            return NoContent();
        }

        [HttpPatch("{id:int}")]
        public async Task<IActionResult> SoftDeleteStaff(int id, bool isDeleted)
        {
            await _service.Staff.SoftDeleteStaffAsync(id, isDeleted);
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteStaff(int id)
        {
            await _service.Staff.DeleteStaffAsync(id);
            return NoContent();
        }
    }
}
