using Contract.Interfaces.IRepository;
using Entity.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Repository.Infrastructure.Data;
using Repository.Repositories;

namespace Repository.Infrastructure.Repository
{
    public class EventRepository : RepositoryBase<Events>, IEventRepository
    {
        public EventRepository(RepositoryContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Events>> GetEventsAsync()
        {
            // ดึงทุก event จาก DB
            //return await _context.Events.ToListAsync();
            return await FindAll(trackChanges: false).ToListAsync();
        }

        public async Task<Events?> GetEventByIdAsync(int id)
        {
            //return await _context.Events
            //    .FirstOrDefaultAsync(e => e.EventId == id);
            return await FindByCondition(e => e.EventId == id, trackChanges: false)
                .FirstOrDefaultAsync();
        }
        public void CreateEvent(Events eventEntity)
        {
            Create(eventEntity);
        }
        public async void DeleteEvent(Events eventEntity)
        {
            Delete(eventEntity);
            await Task.CompletedTask;
        }
    }
}