using Contracts.IRepository;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Data;
using Repositories.Repository.BaseManager;
using Shared.RequestFeatures.Parameters;

namespace Repositories.Repository
{
    public class EventRepository : RepositoryBase<Event>, IEventRepository
    {
        public EventRepository(RepositoryContext context)
            : base(context) { }

        public async Task<IEnumerable<Event>> GetEventsAsync(EventParameter eventParameter, bool trackChanges)
        {
            return await FindAll(trackChanges: false)
                .Include(e => e.Company)
                    .ThenInclude(cc => cc.CompanyContacts)
                .Include(e => e.Package)
                    .ThenInclude(p => p.EquipmentSets)
                        .ThenInclude(es => es.Equipment)
                            .ThenInclude(c => c.Category)
                .Include(e => e.CreatedByStaff)
                    .ThenInclude(s => s.StaffRoles)
                        .ThenInclude(sr => sr.Role)
                .Include(e => e.EventAttachments)
                .Include(e => e.EventStaff)
                    .ThenInclude(es => es.Staff)
                        .ThenInclude(s => s.StaffRoles)
                            .ThenInclude(sr => sr.Role)
                .Include(e => e.EventOutsources)
                    .ThenInclude(os => os.Outsource)
                .Include(e=> e.EventOutsources)
                    .ThenInclude(os => os.Role)
                .Include(e => e.EventExtraEquipments)
                    .ThenInclude(eq => eq.Equipment)
                        .ThenInclude(c => c.Category)
                .ToListAsync();
        }

        public async Task<Event?> GetEventByIdAsync(int id)
        {
            return await FindByCondition(e => e.EventId == id, trackChanges: false)
                    .Include(e => e.Company)
                        .ThenInclude(cc => cc.CompanyContacts)
                        
                    .Include(e => e.Package)
                    .ThenInclude(p => p.EquipmentSets)
                        .ThenInclude(es => es.Equipment)
                            .ThenInclude(c => c.Category)

                    .Include(e => e.CreatedByStaff)
                        .ThenInclude(s => s.StaffRoles)
                            .ThenInclude(sr => sr.Role)

                    .Include(e => e.EventAttachments)

                    .Include(e => e.EventStaff)
                        .ThenInclude(es => es.Staff)
                            .ThenInclude(s => s.StaffRoles)
                                .ThenInclude(sr => sr.Role)

                    .Include(e => e.EventOutsources)
                        .ThenInclude(os => os.Outsource)

                    .Include(e => e.EventOutsources)
                        .ThenInclude(os => os.Role)

                    .Include(e => e.EventExtraEquipments)
                        .ThenInclude(eq => eq.Equipment)
                            .ThenInclude(c => c.Category)
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
