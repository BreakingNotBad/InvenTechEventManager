using Contract.Interfaces.IRepository;
using Entity.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Repository.Infrastructure.Data;
using Repository.Repositories;

namespace Repository.Infrastructure.Repository
{
    public class EventStaffRepository : RepositoryBase<EventStaff>, IEventStaffRepository
    {
        public EventStaffRepository(RepositoryContext context) : base(context)
        {
        }

        public async Task<IEnumerable<EventStaff>> GetEventStaffMembersAsync()
        {
            return await FindAll(trackChanges: false).ToListAsync();
        }

        public async Task<EventStaff?> GetEventStaffByIdAsync(int id)
        {
            return await FindByCondition(e => e.EventStaffId == id, trackChanges: false)
                .FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<EventStaff>> GetEventStaffByEventIdAsync(int eventId)
        {
            return await FindByCondition(es => es.EventId == eventId, trackChanges: false)
                .ToListAsync();
        }
    }
}
