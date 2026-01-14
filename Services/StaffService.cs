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

        public StaffService(IRepositoryManager repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<Staff>> GetStaffMembersAsync(
            string? fullName,
            string? status,
            string? role,
            bool? available,
            DateTime? date,
            string? period)
        {
            var staffs = await _repo.Staff.GetStaffMembersAsync();

            //  search 
            if (!string.IsNullOrWhiteSpace(fullName))
            {
                staffs = staffs.Where(s =>
                    s.FullName.Contains(fullName, StringComparison.OrdinalIgnoreCase) ||
                    (!string.IsNullOrEmpty(s.Email) &&
                        s.Email.Contains(fullName, StringComparison.OrdinalIgnoreCase)) ||
                    (!string.IsNullOrEmpty(s.PhoneNumber) &&
                        s.PhoneNumber.Contains(fullName, StringComparison.OrdinalIgnoreCase))
                );
            }

            //  filter role
            if (!string.IsNullOrWhiteSpace(role))
            {
                staffs = staffs.Where(s =>
                    s.StaffRoles.Any(r =>
                        r.Role.RoleName.Equals(role, StringComparison.OrdinalIgnoreCase))
                );
            }

            ////  filter status (ถ้ามี field Status)
            //if (!string.IsNullOrWhiteSpace(status))
            //{
            //    staffList = staffList.Where(s => s.Status == status);
            //}

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
                    _ => start.AddDays(1)
                };

                staffs = staffs.Where(s =>
                    s.CreatedAt >= start && s.CreatedAt < end);
            }

            return staffs;
        }

        public async Task<Staff?> GetStaffByIdAsync(int id)
        {
            return await _repo.Staff.GetStaffByIdAsync(id);
        }

        public async Task CreateStaffAsync(Staff staff, List<int> roleIds)
        {
            if (roleIds != null && roleIds.Any())
            {
                staff.StaffRoles = new List<StaffRole>();
                foreach (var roleId in roleIds)
                {
                    staff.StaffRoles.Add(new StaffRole { RoleId = roleId });
                }
            }

            _repo.Staff.CreateStaff(staff);
            await _repo.SaveAsync(); 
        }

        public async Task UpdateStaffAsync(int id, UpdateStaffRequest request)
        {
            var existingStaff = await _repo.Staff
                .GetStaffByIdAsync(id);

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

            // update roles (ถ้าส่งมา)
            if (request.RoleIds != null)
            {
                // ลบ role เดิม
                existingStaff.StaffRoles.Clear();

                // เพิ่ม role ใหม่
                foreach (var roleId in request.RoleIds)
                {
                    existingStaff.StaffRoles.Add(new StaffRole
                    {
                        RoleId = roleId,
                        StaffId = existingStaff.StaffId
                    });
                }
            }

            _repo.Staff.UpdateStaff(existingStaff);
            await _repo.SaveAsync();
        }

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
