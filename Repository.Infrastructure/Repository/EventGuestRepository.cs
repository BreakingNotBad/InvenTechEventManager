using Contract.Interfaces.IRepository;
using Entity.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Repository.Infrastructure.Data;
using Repository.Repositories;

namespace Repository.Infrastructure.Repository
{
    public class EventGuestRepository : RepositoryBase<EventGuest>, IEventGuestRepository
    {
        public EventGuestRepository(RepositoryContext context) : base(context)
        {
        }
        public async Task<IEnumerable<EventGuest>> GetEventGuestsAsync()
        {
            return await FindAll(trackChanges: false)
                .ToListAsync();
        }
        public async Task<IEnumerable<EventGuest>> GetByEventIdAsync(int eventId)
        {
            return await FindByCondition(e => e.EventId == eventId, trackChanges: false)
                .ToListAsync();
        }
    }
}
