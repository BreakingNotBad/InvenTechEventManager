using AutoMapper;
using Contract.Interfaces.DTOs;
using Contract.Interfaces.IRepository.BaseManager;
using Entity.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Service.Contract;

namespace Service
{
    public class StaffService : IStaffService
    {
        private readonly IRepositoryManager _repo;
        private readonly IFileService _fileService;
        private readonly IMapper _mapper;

        public StaffService(IRepositoryManager repo, IFileService fileService, IMapper mapper)
        {
            _repo = repo;
            _fileService = fileService;
            _mapper = mapper;
        }

        // GET ALL
        public async Task<IEnumerable<Staff>> GetStaffMembersAsync(
            string? fullName,
            string? status,
            string? role,
            bool? available,
            DateTime? date,
            string? period
        )
        {
            var staffs = await _repo.Staff.GetStaffMembersAsync();

            //  search
            if (!string.IsNullOrWhiteSpace(fullName))
            {
                staffs = staffs.Where(s =>
                    s.FullName.Contains(fullName, StringComparison.OrdinalIgnoreCase)
                    || (
                        !string.IsNullOrEmpty(s.Email)
                        && s.Email.Contains(fullName, StringComparison.OrdinalIgnoreCase)
                    )
                    || (
                        !string.IsNullOrEmpty(s.PhoneNumber)
                        && s.PhoneNumber.Contains(fullName, StringComparison.OrdinalIgnoreCase)
                    )
                );
            }

            //  filter role
            if (!string.IsNullOrWhiteSpace(role))
            {
                staffs = staffs.Where(s =>
                    s.StaffRoles.Any(r =>
                        r.Role.RoleName.Equals(role, StringComparison.OrdinalIgnoreCase)
                    )
                );
            }

            //  filter status (active / inactive)
            if (!string.IsNullOrWhiteSpace(status))
            {
                status = status.Trim().ToLower();

                if (status == "active" || status == "false")
                {
                    staffs = staffs.Where(s => !s.IsDeleted);
                }
                else if (status == "inactive" || status == "true")
                {
                    staffs = staffs.Where(s => s.IsDeleted);
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

                staffs = staffs.Where(s => s.CreatedAt >= start && s.CreatedAt < end);
            }

            return staffs;
        }

        // GET BY ID
        public async Task<Staff?> GetStaffByIdAsync(int id)
        {
            return await _repo.Staff.GetStaffByIdAsync(id);
        }

        // CREATE
        public async Task<Staff> CreateStaffAsync(
            CreateStaffDto staffDto,
            Stream? avatarStream,
            string? avatarFileName
        )
        {
            // 1. จัดการเรื่องไฟล์
            string? avatarPath = null;

            // เช็คว่ามี Stream ส่งมาไหม
            if (
                avatarStream != null
                && avatarStream.Length > 0
                && !string.IsNullOrEmpty(avatarFileName)
            )
            {
                // เรียก FileService ให้ช่วยเซฟ (โดยส่ง Stream ไป)
                avatarPath = await _fileService.SaveFileAsync(
                    avatarStream,
                    avatarFileName,
                    "Staff" // ชื่อ Folder ย่อย
                );
            }

            // 2. สร้าง Entity (Mapping)
            var staffEntity = _mapper.Map<Staff>(staffDto);
            staffEntity.Avatar = avatarPath;

            // 3. จัดการ Role
            if (staffDto.RoleIds != null && staffDto.RoleIds.Any())
            {
                staffEntity.StaffRoles = new List<StaffRole>();
                foreach (var roleId in staffDto.RoleIds)
                {
                    staffEntity.StaffRoles.Add(new StaffRole { RoleId = roleId });
                }
            }

            // 4. บันทึกลง Database
            _repo.Staff.CreateStaff(staffEntity);
            await _repo.SaveAsync();

            return staffEntity;
        }

        // UPDATE
        public async Task UpdateStaffAsync(
            int id,
            UpdateStaffDto dto,
            Stream? avatarStream,
            string? avatarFileName
        )
        {
            var staff = await _repo.Staff.GetStaffByIdAsync(id);

            if (staff == null)
                throw new KeyNotFoundException($"Staff with id {id} not found.");

            // update fields
            _mapper.Map(dto, staff);
            staff.UpdatedAt = DateTime.UtcNow;

            // Delete Avatar
            if (dto.DeleteAvatar == true)
            {
                if (!string.IsNullOrEmpty(staff.Avatar))
                {
                    await _fileService.DeleteFileAsync(staff.Avatar);
                }
                staff.Avatar = null;
            }
            // avatar
            else if (avatarStream != null && !string.IsNullOrEmpty(avatarFileName))
            {
                // 3.1 (Optional) ลบไฟล์เก่าทิ้งก่อน ถ้าต้องการประหยัดพื้นที่
                if (!string.IsNullOrEmpty(staff.Avatar))
                    await _fileService.DeleteFileAsync(staff.Avatar);

                // 3.2 เซฟไฟล์ใหม่
                var newAvatarPath = await _fileService.SaveFileAsync(
                    avatarStream,
                    avatarFileName,
                    "Staff"
                );

                // 3.3 อัปเดต Path ใน DB
                staff.Avatar = newAvatarPath;
            }

            // roles (เฉพาะเมื่อส่งมา)
            if (dto.RoleIds != null)
            {
                staff.StaffRoles.Clear();
                foreach (var roleId in dto.RoleIds)
                {
                    staff.StaffRoles.Add(
                        new StaffRole { StaffId = staff.StaffId, RoleId = roleId }
                    );
                }
            }

            // ✅ soft delete
            if (dto.IsDeleted.HasValue)
            {
                staff.IsDeleted = dto.IsDeleted.Value;
            }

            _repo.Staff.UpdateStaff(staff);
            await _repo.SaveAsync();
        }

        // DELETE
        public async Task DeleteStaffAsync(int id)
        {
            var exinstingStaff = await _repo.Staff.GetStaffByIdAsync(id);

            if (exinstingStaff == null)
            {
                throw new KeyNotFoundException($"Staff with id: {id} does not exist.");
            }

            _repo.Staff.DeleteStaff(exinstingStaff);
            await _repo.SaveAsync();
        }

        public async Task SoftDeleteStaffAsync(int id, bool isDeleted)
        {
            var existingStaff = await _repo.Staff.GetStaffByIdAsync(id);
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
