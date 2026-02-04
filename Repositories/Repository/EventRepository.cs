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

        public async Task<IEnumerable<Event>> GetEventsAsync(
            EventParameter eventParameter,
            bool trackChanges)
        {
            var query = FindAll(trackChanges);

            // EventName
            if (!string.IsNullOrWhiteSpace(eventParameter.EventName))
            {
                var keyword = eventParameter.EventName.ToLower();
                query = query.Where(e =>e.EventName.ToLower().Contains(keyword));
            }

            // EventType
            if (eventParameter.EventType.HasValue)
            {
                var eventTypeValue = (int)eventParameter.EventType.Value;
                query = query.Where(e =>
                    (int)e.EventType == eventTypeValue);
            }

            // TimePeriod
            if (eventParameter.Period.HasValue)
            {
                var periodValue = (int)eventParameter.Period.Value;
                query = query.Where(e => (int)e.Period == periodValue);
            }

            //// Status
            //if (!string.IsNullOrWhiteSpace(eventParameter.Status))
            //{
            //    var status = eventParameter.Status.ToLower();
            //    query = query.Where(e => e.Status.ToLower() == status);
            //}

            // CompanyName
            if (!string.IsNullOrWhiteSpace(eventParameter.CompanyName))
            {
                var companyName = eventParameter.CompanyName.ToLower();
                query = query.Where(e =>e.Company.CompanyName.ToLower().Contains(companyName));
            }

            // FullName (Staff หรือ Outsource)
            if (!string.IsNullOrWhiteSpace(eventParameter.FullName))
            {
                var name = eventParameter.FullName.ToLower();

                query = query.Where(e =>
                    e.EventStaff.Any(es =>(es.Staff.FullName).ToLower().Contains(name))
                    ||
                    e.EventOutsources.Any(os =>(os.Outsource.FullName).ToLower().Contains(name))
                );
            }

            return await query
                .Include(e => e.Company)
                    .ThenInclude(c => c.CompanyContacts)

                .Include(e => e.Package)
                    .ThenInclude(p => p.EquipmentSets)
                        .ThenInclude(es => es.Equipment)
                            .ThenInclude(eq => eq.Category)

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
                        .ThenInclude(eq => eq.Category)

                .ToListAsync();
        }

        public async Task<Event?> GetEventByIdAsync(int id, bool trackChanges)
        {
            return await FindByCondition(e => e.EventId == id, trackChanges)
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

        public void UpdateEvent(Event eventEntity)
        {
            Update(eventEntity);
        }
    }
}
