using AutoMapper;
using Contracts.DTOs;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Presentation.Requests.Staff;
using Service.Contracts.Manager;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/staff")]
    public class StaffController : ControllerBase
    {
        private readonly IServiceManager _service;
        private readonly IMapper _mapper;

        public StaffController(IServiceManager service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
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
            var staffDto = _mapper.Map<CreateStaffDto>(request);

            Stream? stream = null;
            string? fileName = null;

            if (request.AvatarFile != null)
            {
                stream = request.AvatarFile.OpenReadStream();
                fileName = request.AvatarFile.FileName;
            }

            using (stream)
            {
                var resultStaff = await _service.Staff.CreateStaffAsync(staffDto, stream, fileName);

                return CreatedAtAction(
                    nameof(GetStaffById),
                    new { id = resultStaff.StaffId },
                    resultStaff
                );
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateStaff(int id, [FromForm] UpdateStaffRequest request)
        {
            var dto = _mapper.Map<UpdateStaffDto>(request);

            Stream? stream = null;
            string? fileName = null;

            if (request.AvatarFile != null)
            {
                stream = request.AvatarFile.OpenReadStream();
                fileName = request.AvatarFile.FileName;
            }

            using (stream)
            {
                await _service.Staff.UpdateStaffAsync(id, dto, stream, fileName);
            }

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
