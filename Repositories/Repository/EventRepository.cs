using Contracts.IRepository;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Data;
using Repositories.Repository.BaseManager;

namespace Repositories.Repository
{
    public class EventRepository : RepositoryBase<Event>, IEventRepository
    {
        public EventRepository(RepositoryContext context)
            : base(context) { }

        public async Task<IEnumerable<Event>> GetEventsAsync()
        {
            // ดึงทุก event จาก DB
            //return await _context.Events.ToListAsync();
            return await FindAll(trackChanges: false).ToListAsync();
        }

        public async Task<Event?> GetEventByIdAsync(int id)
        {
            //return await _context.Events
            //    .FirstOrDefaultAsync(e => e.EventId == id);
            return await FindByCondition(e => e.EventId == id, trackChanges: false)
                .FirstOrDefaultAsync();
        }

        public void CreateEvent(Event eventEntity)
        {
            Create(eventEntity);
        }

        public async void DeleteEvent(Event eventEntity)
        {
            Delete(eventEntity);
            await Task.CompletedTask;
        }
    }
}
