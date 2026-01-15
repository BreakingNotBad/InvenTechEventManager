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

        public StaffService(IRepositoryManager repo, IFileService fileService)
        {
            _repo = repo;
            _fileService = fileService;
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
            var staffEntity = new Staff
            {
                FullName = staffDto.FullName,
                Email = staffDto.Email,
                PhoneNumber = staffDto.PhoneNumber,
                Avatar = avatarPath, // เอา Path ที่ได้จากการเซฟมาใส่
            };

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
        public async Task UpdateStaffAsync(int id, UpdateStaffRequest request)
        {
            var existingStaff = await _repo.Staff.GetStaffByIdAsync(id);

            if (existingStaff == null)
            {
                throw new KeyNotFoundException($"Staff with id {id} not found.");
            }

            // update staff fields
            existingStaff.FullName = request.FullName;
            existingStaff.Email = request.Email;
            existingStaff.PhoneNumber = request.PhoneNumber;
            existingStaff.Avatar = request.Avatar;
            existingStaff.UpdatedAt = DateTime.UtcNow;
            existingStaff.IsDeleted = request.IsDeleted;

            // update roles (ถ้าส่งมา)
            if (request.RoleIds != null)
            {
                // ลบ role เดิม
                existingStaff.StaffRoles.Clear();

                // เพิ่ม role ใหม่
                foreach (var roleId in request.RoleIds)
                {
                    existingStaff.StaffRoles.Add(
                        new StaffRole { RoleId = roleId, StaffId = existingStaff.StaffId }
                    );
                }
            }

            _repo.Staff.UpdateStaff(existingStaff);
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
