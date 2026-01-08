using Contract.Interfaces.IRepository;
using Entity.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Repository.Infrastructure.Data;
using Repository.Infrastructure.Repository.BaseManager;

namespace Repository.Infrastructure.Repository
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

        public async Task<Staff?> GetStaffByIdAsync(int id)
        {
            return await FindByCondition(s => s.StaffId == id, trackChanges: false)
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
