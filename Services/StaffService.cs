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

        public async Task<IEnumerable<Staff>> GetStaffMembersAsync(string? query,
            string? status,
            string? role,
            bool? available,
            DateTime? date,
            string? period)
        {
            var staffs = await _repo.Staff.GetStaffMembersAsync();

            //  search (case-insensitive)
            if (!string.IsNullOrWhiteSpace(query))
            {
                staffs = staffs.Where(s =>
                    s.FullName.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                    (!string.IsNullOrEmpty(s.Email) &&
                        s.Email.Contains(query, StringComparison.OrdinalIgnoreCase)) ||
                    (!string.IsNullOrEmpty(s.PhoneNumber) &&
                        s.PhoneNumber.Contains(query, StringComparison.OrdinalIgnoreCase))
                );
            }

            //  filter role (case-insensitive ด้วย)
            if (!string.IsNullOrWhiteSpace(role))
            {
                staffs = staffs.Where(s =>
                    s.StaffRoles.Any(r =>
                        r.Role.RoleName.Equals(role, StringComparison.OrdinalIgnoreCase))
                );
            }

            //// 📌 filter status (ถ้ามี field Status)
            //if (!string.IsNullOrWhiteSpace(status))
            //{
            //    staffList = staffList.Where(s => s.Status == status);
            //}

            //// ✅ filter available
            //if (available.HasValue)
            //{
            //    staffList = staffList.Where(s => s.IsAvailable == available.Value);
            //}

            // 📅 filter date / time_period
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

        public async Task CreateStaffAsync(Staff staff)
        {
            _repo.Staff.CreateStaff(staff);
            await _repo.SaveAsync();
        }

        public async Task UpdateStaffAsync(int id, Staff staff)
        {
            var exinstingStaff = await _repo.Staff.GetStaffByIdAsync(id);

            if (exinstingStaff == null)
            {
                throw new KeyNotFoundException($"Staff with id: {id} does not exist.");
            }

            exinstingStaff.FullName = staff.FullName;
            exinstingStaff.Email = staff.Email;
            exinstingStaff.PhoneNumber = staff.PhoneNumber;
            exinstingStaff.Avatar = staff.Avatar;
            exinstingStaff.UpdatedAt = DateTime.UtcNow;

            _repo.Staff.UpdateStaff(exinstingStaff);
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
    }
}
