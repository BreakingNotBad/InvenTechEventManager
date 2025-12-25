using Contract.Interfaces.IRepository;
using Entity.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Repository.Infrastructure.Data;
using Repository.Repositories;

namespace Repository.Infrastructure.Repository
{
    public class StaffRepository : RepositoryBase<Staff>, IStaffRepository
    {
        public StaffRepository(RepositoryContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Staff>> GetStaffMembersAsync()
        {
            return await FindAll(trackChanges: false).ToListAsync();
        }

        public async Task<Staff?> GetStaffByIdAsync(int id)
        {
            return await FindByCondition(e => e.StaffId == id, trackChanges: false)
                .FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<Staff>> GetStaffByEventIdAsync(int eventId)
        {
            return await FindByCondition(es => es.EventId == eventId, trackChanges: false)
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

        public async Task<IEnumerable<Staff>> GetStaffActiveAsync()
        {
            return await FindAll(trackChanges: false).ToListAsync();
        }
    }
}
