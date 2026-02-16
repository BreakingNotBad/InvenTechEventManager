using Contracts.IRepository;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Data;
using Repositories.Repository.BaseManager;
using Shared.RequestFeatures.Enums;
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
                query = query.Where(e => e.EventName.ToLower().Contains(keyword));
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

            // Status
            if (!string.IsNullOrWhiteSpace(eventParameter.Status))
            {
                var status = eventParameter.Status.ToLower();
                query = query.Where(e => e.EventStatus.ToString().ToLower().Contains(status));
            }

            // CompanyName
            if (!string.IsNullOrWhiteSpace(eventParameter.CompanyName))
            {
                var companyName = eventParameter.CompanyName.ToLower();
                query = query.Where(e => e.Company.CompanyName.ToLower().Contains(companyName));
            }

            // FullName (Staff หรือ Outsource)
            if (!string.IsNullOrWhiteSpace(eventParameter.FullName))
            {
                var name = eventParameter.FullName.ToLower();

                query = query.Where(e =>
                    e.EventStaff.Any(es => (es.Staff.FullName).ToLower().Contains(name))
                    ||
                    e.EventOutsources.Any(os => (os.Outsource.FullName).ToLower().Contains(name))
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

                .Include(e => e.RoleRequirements)
                    .ThenInclude(rr => rr.Role)

                .Include(e => e.EventStaff)
                    .ThenInclude(es => es.Staff)
                        .ThenInclude(s => s.StaffRoles)
                            .ThenInclude(sr => sr.Role)

                .Include(e => e.EventStaff)
                    .ThenInclude(es => es.Role)

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

                    .Include(e => e.RoleRequirements)
                        .ThenInclude(rr => rr.Role)

                    .Include(e => e.EventStaff)
                        .ThenInclude(es => es.Staff)
                            .ThenInclude(s => s.StaffRoles)
                                .ThenInclude(sr => sr.Role)

                    .Include(e => e.EventStaff)
                        .ThenInclude(es => es.Role)

                    .Include(e => e.EventOutsources)
                        .ThenInclude(os => os.Outsource)

                    .Include(e => e.EventOutsources)
                        .ThenInclude(os => os.Role)

                    .Include(e => e.EventExtraEquipments)
                        .ThenInclude(eq => eq.Equipment)
                            .ThenInclude(c => c.Category)
                    .FirstOrDefaultAsync();
        }

        // เช็คว่าพนักงานว่างหรือไม่ (สร้างงานใหม่)
        //public async Task<bool> IsStaffAvailableAsync(
        //    int staffId,
        //    DateOnly date,
        //    TimePeriod period)
        //{
        //    return !await FindAll(false)
        //        .AnyAsync(e =>
        //            e.MeetingDate == date &&
        //            e.Period == period &&
        //            e.EventStaff.Any(es => es.StaffId == staffId)
        //        );
        //}

        //public async Task<bool> IsOutsourceAvailableAsync(
        //    int outsourceId,
        //    DateOnly date,
        //    TimePeriod period)
        //{
        //    return !await FindAll(false)
        //        .AnyAsync(e =>
        //            e.MeetingDate == date &&
        //            e.Period == period &&
        //            e.EventOutsources.Any(os => os.OutsourceId == outsourceId)
        //        );
        //}


        // เช็คว่าพนักงานว่างหรือไม่ (แก้ไขงาน โดยไม่เอางานตัวเองไปคิด)
        //public async Task<bool> IsStaffAvailableAsync(
        //    int staffId,
        //    DateOnly date,
        //    TimePeriod period,
        //    int excludeEventId)
        //{
        //    return !await FindAll(false)
        //        .AnyAsync(e =>
        //            e.EventId != excludeEventId &&
        //            e.MeetingDate == date &&
        //            e.Period == period &&
        //            e.EventStaff.Any(es => es.StaffId == staffId)
        //        );
        //}

        //public async Task<bool> IsOutsourceAvailableAsync(
        //    int outsourceId,
        //    DateOnly date,
        //    TimePeriod period,
        //    int excludeEventId)
        //{
        //    return !await FindAll(false)
        //        .AnyAsync(e =>
        //            e.EventId != excludeEventId &&
        //            e.MeetingDate == date &&
        //            e.Period == period &&
        //            e.EventOutsources.Any(os => os.OutsourceId == outsourceId)
        //        );
        //}

        //public async Task<bool> CheckAvailabilityAsync(
        //    int? staffId,
        //    int? outsourceId,
        //    DateOnly meetingDate,
        //    TimePeriod period,
        //    int? excludeEventId)
        //{
        //    var query = FindAll(false)
        //        .Where(e =>
        //            e.MeetingDate == meetingDate &&
        //            e.Period == period);

        //    if (excludeEventId.HasValue)
        //        query = query.Where(e => e.EventId != excludeEventId.Value);

        //    if (staffId.HasValue)
        //    {
        //        query = query.Where(e =>
        //            e.EventStaff.Any(es => es.StaffId == staffId.Value));
        //    }

        //    if (outsourceId.HasValue)
        //    {
        //        query = query.Where(e =>
        //            e.EventOutsources.Any(os => os.OutsourceId == outsourceId.Value));
        //    }

        //    // ถ้ามี event ซ้ำ = ไม่ว่าง
        //    return !await query.AnyAsync();
        //}

        public async Task<Event?> GetConflictEventByStaffAsync(
            int staffId,
            DateOnly date,
            TimePeriod period)
        {
            return await FindAll(true)
                .Include(e => e.EventStaff)
                .FirstOrDefaultAsync(e =>
                    e.MeetingDate == date &&
                    e.Period == period &&
                    e.EventStaff.Any(es => es.StaffId == staffId)
                );
        }

        public async Task<Event?> GetConflictEventByStaffAsync(
            int staffId,
            DateOnly date,
            TimePeriod period,
            int excludeEventId)
        {
            return await FindAll(true)
                .Include(e => e.EventStaff)
                .FirstOrDefaultAsync(e =>
                    e.EventId != excludeEventId &&
                    e.MeetingDate == date &&
                    e.Period == period &&
                    e.EventStaff.Any(es => es.StaffId == staffId)
                );
        }

        public async Task<Event?> GetConflictEventByOutsourceAsync(
            int outsourceId,
            DateOnly date,
            TimePeriod period)
        {
            return await FindAll(true)
                .Include(e => e.EventOutsources)
                .FirstOrDefaultAsync(e =>
                    e.MeetingDate == date &&
                    e.Period == period &&
                    e.EventOutsources.Any(os => os.OutsourceId == outsourceId)
                );
        }

        public async Task<Event?> GetConflictEventByOutsourceAsync(
            int outsourceId,
            DateOnly date,
            TimePeriod period,
            int excludeEventId)
        {
            return await FindAll(true)
                .Include(e => e.EventOutsources)
                .FirstOrDefaultAsync(e =>
                    e.EventId != excludeEventId &&
                    e.MeetingDate == date &&
                    e.Period == period &&
                    e.EventOutsources.Any(os => os.OutsourceId == outsourceId)
                );
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
