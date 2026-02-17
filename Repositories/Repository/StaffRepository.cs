using Contracts.IRepository;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Data;
using Repositories.Repository.BaseManager;
using Shared.RequestFeatures.Parameters;

namespace Repositories.Repository
{
    public class StaffRepository : RepositoryBase<Staff>, IStaffRepository
    {
        public StaffRepository(RepositoryContext context)
            : base(context) { }

        public async Task<IEnumerable<Staff>> GetStaffMembersAsync(
            StaffParameter staffParameter,
            bool trackChanges
        )
        {
            // 1. เริ่มต้น Query
            var staffList = FindAll(trackChanges);

            // 2. Filter: FullName
            if (!string.IsNullOrWhiteSpace(staffParameter.FullName))
            {
                var searchTerm = staffParameter.FullName.Trim().ToLower();
                staffList = staffList.Where(s => s.FullName.ToLower().Contains(searchTerm));
            }

            // 3. Filter: Role (Many-to-Many / One-to-Many relationship)
            if (!string.IsNullOrWhiteSpace(staffParameter.Role))
            {
                // EF Core จะแปลง .Any() เป็น EXISTS ใน SQL
                staffList = staffList.Where(s =>
                    s.StaffRoles.Any(r => r.Role.RoleName == staffParameter.Role)
                );
            }

            // 4. Filter: IsDeleted
            if (staffParameter.IsDeleted.HasValue)
            {
                staffList = staffList.Where(s => s.IsDeleted == staffParameter.IsDeleted.Value);
            }

            // 5. Filter: Availability
            if (staffParameter.Date.HasValue)
            {
                staffList = staffList
                    .Include(s =>
                        s.EventStaff!.Where(es => es.Event.MeetingDate == staffParameter.Date.Value)
                    )
                        .ThenInclude(es => es.Event);
            }

            // 6. Execute Query + Include Data
            return await staffList
                .Include(s => s.StaffRoles)
                    .ThenInclude(sr => sr.Role)
                .ToListAsync();
        }

        public async Task<Staff?> GetStaffByIdAsync(int id, bool trackchanges)
        {
            return await FindByCondition(s => s.StaffId == id, trackchanges)
                .Include(s => s.StaffRoles)
                    .ThenInclude(sr => sr.Role)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Staff>> GetStaffByEventIdAsync(int eventId)
        {
            return await FindByCondition(es => es.StaffId == eventId, trackChanges: false)
                .ToListAsync();
        }

        public async Task<bool> AllStaffIdsExistAsync(IEnumerable<int> staffIds)
        {
            if (staffIds == null)
                return true;

            var ids = staffIds.Distinct().ToList();
            if (!ids.Any())
                return true;

            var countInDb = await FindByCondition(
                    s => ids.Contains(s.StaffId) && !s.IsDeleted,
                    trackChanges: false
                )
                .CountAsync();

            // ถ้าจำนวนใน DB เท่ากับจำนวนที่ส่งมา = มีอยู่จริงทั้งหมด
            return countInDb == ids.Count;
        }

        public async Task<Staff?> GetStaffForLoginAsync(string email)
        {
            return await FindByCondition(s =>
                    s.Email == email &&
                    !s.IsDeleted,
                    false)
                .Include(s => s.StaffRoles)
                    .ThenInclude(sr => sr.Role)
                .Include(s => s.StaffPermissions)
                    .ThenInclude(sp => sp.Permission)
                .FirstOrDefaultAsync();
        }

        public async Task<Staff?> GetByResetTokenAsync(string token)
        {
            return await FindByCondition(
                s => s.PasswordResetToken == token,
                trackChanges: true
            ).FirstOrDefaultAsync();
        }

        public async Task<Staff?> GetByEmailAsync(string email)
        {
            return await FindByCondition(
                s => s.Email == email,
                trackChanges: true
            ).FirstOrDefaultAsync();
        }


        public void CreateStaff(Staff staff)
        {
            Create(staff);
        }

        public void UpdateStaff(Staff staff)
        {
            Update(staff);
        }

        public void DeleteStaff(Staff staff)
        {
            Delete(staff);
        }
    }
}
