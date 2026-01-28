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

        public async Task<IEnumerable<Staff>> GetStaffMembersAsync(StaffParameter staffParameter, bool trackChanges)
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

            // 4. Filter: Status (Active/Inactive -> IsDeleted)
            if (!string.IsNullOrWhiteSpace(staffParameter.Status))
            {
                var status = staffParameter.Status.Trim().ToLower();
                if (status == "active" || status == "false")
                {
                    staffList = staffList.Where(s => !s.IsDeleted);
                }
                else if (status == "inactive" || status == "true")
                {
                    staffList = staffList.Where(s => s.IsDeleted);
                }
            }

            // 5. Filter: Available
            if (staffParameter.Available.HasValue)
            {
                // สมมติว่ามี field IsAvailable หรือ Logic ที่ตรงกัน
                // staffList = staffList.Where(s => s.IsAvailable == parameters.Available.Value);
            }

            // 6. Filter: Date & Period
            if (staffParameter.Date.HasValue && !string.IsNullOrWhiteSpace(staffParameter.Period))
            {
                var start = staffParameter.Date.Value.Date;
                DateTime end;

                // คำนวณช่วงเวลา
                switch (staffParameter.Period.ToLower())
                {
                    case "week":
                        end = start.AddDays(7);
                        break;
                    case "month":
                        end = start.AddMonths(1);
                        break;
                    case "day":
                    default:
                        end = start.AddDays(1);
                        break;
                }

                // กรองช่วงเวลา (CreatedAt >= start AND CreatedAt < end)
                staffList = staffList.Where(s => s.CreatedAt >= start && s.CreatedAt < end);
            }

            // 7. Execute Query + Include Data
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
