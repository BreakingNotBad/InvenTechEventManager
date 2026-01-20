using Contracts.IRepository;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Data;
using Repositories.Repository.BaseManager;

namespace Repositories.Repository
{
    public class StaffRepository : RepositoryBase<Staff>, IStaffRepository
    {
        public StaffRepository(RepositoryContext context)
            : base(context) { }

        public async Task<IEnumerable<Staff>> GetStaffMembersAsync()
        {
            return await FindAll(trackChanges: false)
                .Include(s => s.StaffRoles)
                    .ThenInclude(sr => sr.Role)
                .ToListAsync();
        }

        public async Task<Staff?> GetStaffByIdAsync(int id,bool trackchanges)
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
