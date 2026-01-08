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
            var staffs = _repo.Staff.GetStaffMembersAsync();

            var staffList = await staffs;

            //  search (q)
            if (!string.IsNullOrWhiteSpace(query))
            {
                staffList = staffList.Where(s =>
                    s.FullName.Contains(query) ||
                    (s.Email != null && s.Email.Contains(query)) ||
                    (s.PhoneNumber != null && s.PhoneNumber.Contains(query)));
            }

            //  filter role
            if (!string.IsNullOrWhiteSpace(role))
            {
                staffList = staffList.Where(s =>
                    s.StaffRoles.Any(r => r.Role.RoleName == role));
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

                staffList = staffList.Where(s =>
                    s.CreatedAt >= start && s.CreatedAt < end);
            }

            return staffList.ToList();
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
