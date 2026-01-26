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
            return await FindAll(trackChanges: false)
                .Include(e => e.Company)
                .Include(e => e.Package)
                    .ThenInclude(p => p.EquipmentSets)
                        .ThenInclude(es => es.Equipment)
                .Include(e => e.CreatedByStaff)
                .Include(e => e.EventAttachments)
                .Include(e => e.EventStaff)
                    .ThenInclude(es => es.Staff)
                .Include(e => e.EventOutsources)
                .Include(e => e.EventExtraEquipments)
                    .ThenInclude(eq => eq.Equipment)
                .ToListAsync();
        }

        public async Task<Event?> GetEventByIdAsync(int id)
        {
            //return await _context.Events
            //    .FirstOrDefaultAsync(e => e.EventId == id);
            return await FindByCondition(e => e.EventId == id, trackChanges: false)
                    .Include(e => e.Company)
                    .Include(e => e.Package)
                    .ThenInclude(p => p.EquipmentSets)
                        .ThenInclude(es => es.Equipment)
                    .Include(e => e.CreatedByStaff)
                    .Include(e => e.EventAttachments)
                    .Include(e => e.EventStaff)
                        .ThenInclude(es => es.Staff)
                    .Include(e => e.EventOutsources)
                    .Include(e => e.EventExtraEquipments)
                        .ThenInclude(eq => eq.Equipment)
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
