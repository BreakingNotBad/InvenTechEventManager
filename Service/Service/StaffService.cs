using AutoMapper;
using Contracts.IRepository.BaseManager;
using Entities.Exceptions;
using Entities.Models;
using FluentValidation;
using Service.Contracts.DTOs.Staff;
using Service.Contracts.IService;

namespace Service.Service
{
    public class StaffService : IStaffService
    {
        private readonly IRepositoryManager _repo;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;
        private readonly IValidator<CreateStaffDto> _createValidator;
        private readonly IValidator<UpdateStaffDto> _updateValidator;

        public StaffService(
            IRepositoryManager repo,
            IFileService fileService,
            IMapper mapper,
            IValidator<CreateStaffDto> createValidator,
            IValidator<UpdateStaffDto> updateValidator
        )
        {
            _repo = repo;
            _fileService = fileService;
            _mapper = mapper;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        // GET ALL
        public async Task<IEnumerable<StaffDto>> GetStaffMembersAsync(
            string? fullName,
            string? status,
            string? role,
            bool? available,
            DateTime? date,
            string? period
        )
        {
            var staffList = await _repo.Staff.GetStaffMembersAsync();

            //  search
            if (!string.IsNullOrWhiteSpace(fullName))
            {
                staffList = staffList.Where(s =>
                    s.FullName?.ToLower().Contains(fullName.Trim().ToLower()) == true
                );
            }

            //  filter role
            if (!string.IsNullOrWhiteSpace(role))
            {
                staffList = staffList.Where(s =>
                    s.StaffRoles.Any(r => r.Role.RoleName.Equals(role))
                );
            }

            //  filter status (active / inactive)
            if (!string.IsNullOrWhiteSpace(status))
            {
                status = status.Trim().ToLower();

                if (status == "active" || status == "false")
                {
                    staffList = staffList.Where(s => !s.IsDeleted);
                }
                else if (status == "inactive" || status == "true")
                {
                    staffList = staffList.Where(s => s.IsDeleted);
                }
            }

            //// filter available
            //if (available.HasValue)
            //{
            //    staffList = staffList.Where(s => s.IsAvailable == available.Value);
            //}

            //  filter date / time_period
            if (date.HasValue && !string.IsNullOrWhiteSpace(period))
            {
                var start = date.Value.Date;
                DateTime end = period.ToLower() switch
                {
                    "day" => start.AddDays(1),
                    "week" => start.AddDays(7),
                    "month" => start.AddMonths(1),
                    _ => start.AddDays(1),
                };

                staffList = staffList.Where(s => s.CreatedAt >= start && s.CreatedAt < end);
            }

            // แปลงข้อมูลจาก Entity เป็น Dto
            var staffResponse = _mapper.Map<IEnumerable<StaffDto>>(staffList);

            return staffResponse;
        }

        // GET BY ID
        public async Task<StaffDto?> GetStaffByIdAsync(int id)
        {
            var existingStaff = await _repo.Staff.GetStaffByIdAsync(id, false);

            if (existingStaff == null)
            {
                throw new NotFoundException(nameof(Staff), id);
            }

            // แปลงข้อมูลจาก Entity เป็น Dto
            var staffResponse = _mapper.Map<StaffDto>(existingStaff);

            return staffResponse;
        }

        // CREATE
        public async Task<StaffDto> CreateStaffAsync(CreateStaffDto staffDto)
        {
            await _createValidator.ValidateAndThrowAsync(staffDto);

            // แปลงข้อมูลจาก Dto เป็น Entity
            var newStaff = _mapper.Map<Staff>(staffDto);

            // เอา Path จาก Dto ใส่ Entity
            newStaff.Avatar = staffDto.Avatar;

            // จัดการ Role
            if (staffDto.RoleIds != null) // ถ้ามีการกำหนด RoleIds มา
            {
                newStaff.StaffRoles = staffDto
                    .RoleIds // รับ RoleIds จาก DTO
                    .Select(roleId => new StaffRole { RoleId = roleId }) // สร้าง StaffRole ใหม่สำหรับแต่ละ RoleId
                    .ToList();
            }

            _repo.Staff.CreateStaff(newStaff);
            await _repo.SaveAsync();

            // แปลงข้อมูลจาก Entity เป็น Dto
            var staffResponse = _mapper.Map<StaffDto>(newStaff);

            return staffResponse;
        }

        // UPDATE
        public async Task UpdateStaffAsync(int id, UpdateStaffDto staffDto)
        {
            await _updateValidator.ValidateAndThrowAsync(staffDto);

            var existingStaff = await _repo.Staff.GetStaffByIdAsync(id, true);

            if (existingStaff == null)
            {
                throw new NotFoundException(nameof(Staff), id);
            }

            string? oldAvatar = existingStaff.Avatar;

            // แปลงข้อมูลจาก Entity เป็น Dto
            _mapper.Map(staffDto, existingStaff);

            // สั่งลบรูป (DeleteAvatar = true)
            if (staffDto.DeleteAvatar == true)
            {
                // เช็คจาก oldAvatar ที่เราเก็บไว้ตอนแรก
                if (!string.IsNullOrEmpty(oldAvatar))
                {
                    await _fileService.DeleteFileAsync(oldAvatar);
                }

                // สั่งให้ Entity เป็น null (ไม่ใช่ DTO)
                existingStaff.Avatar = null;
            }

            // มีรูปใหม่มาแทน (staffDto.Avatar มีค่า path ใหม่มา)
            if (staffDto.Avatar != null)
            {
                existingStaff.Avatar = staffDto.Avatar;
            }

            // Update Roles
            if (staffDto.RoleIds != null)
            {
                existingStaff.StaffRoles.Clear(); // ลบของเก่า
                foreach (var roleId in staffDto.RoleIds)
                {
                    existingStaff.StaffRoles.Add(
                        new StaffRole { StaffId = existingStaff.StaffId, RoleId = roleId }
                    );
                }
            }

            // เช็คการลบ (Soft Delete)
            if (staffDto.IsDeleted.HasValue)
            {
                existingStaff.IsDeleted = staffDto.IsDeleted.Value; // อัปเดตสถานะ
            }

            _repo.Staff.UpdateStaff(existingStaff);
            await _repo.SaveAsync();
        }

        // DELETE
        public async Task DeleteStaffAsync(int id)
        {
            var exinstingStaff = await _repo.Staff.GetStaffByIdAsync(id, true);

            if (exinstingStaff == null)
            {
                throw new KeyNotFoundException($"Staff with id: {id} does not exist.");
            }

            _repo.Staff.DeleteStaff(exinstingStaff);
            await _repo.SaveAsync();
        }

        public async Task SoftDeleteStaffAsync(int id, bool isDeleted)
        {
            var existingStaff = await _repo.Staff.GetStaffByIdAsync(id, true);
            if (existingStaff == null)
            {
                throw new KeyNotFoundException($"Staff with id {id} not found.");
            }
            existingStaff.IsDeleted = isDeleted;
            _repo.Staff.UpdateStaff(existingStaff);
            await _repo.SaveAsync();
        }
    }
}
