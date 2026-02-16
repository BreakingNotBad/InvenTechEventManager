using Contracts.IRepository.BaseManager;
using Entities.Models;
using Shared.RequestFeatures.Enums;
using Shared.RequestFeatures.Parameters;

namespace Contracts.IRepository;

public interface IEventRepository : IRepositoryBase<Event>
{
    Task<IEnumerable<Event>> GetEventsAsync(EventParameter eventParameter, bool trackChanges);
    Task<Event?> GetEventByIdAsync(int id, bool trackChanges);

    //Task<bool> IsStaffAvailableAsync(
    //    int staffId,
    //    DateOnly date,
    //    TimePeriod period);

    //Task<bool> IsOutsourceAvailableAsync(
    //    int outsourceId,
    //    DateOnly date,
    //    TimePeriod period);

    // สำหรับ Update (exclude ตัวเอง)
    //Task<bool> IsStaffAvailableAsync(
    //    int staffId,
    //    DateOnly date,
    //    TimePeriod period,
    //    int excludeEventId);

    //Task<bool> IsOutsourceAvailableAsync(
    //    int outsourceId,
    //    DateOnly date,
    //    TimePeriod period,
    //    int excludeEventId);

    //Task<bool> CheckAvailabilityAsync(
    //    int? staffId,
    //    int? outsourceId,
    //    DateOnly meetingDate,
    //    TimePeriod period,
    //    int? excludeEventId);

    Task<Event?> GetConflictEventByStaffAsync(
        int staffId,
        DateOnly date,
        TimePeriod period);

    Task<Event?> GetConflictEventByOutsourceAsync(
        int outsourceId,
        DateOnly date,
        TimePeriod period);

    Task<Event?> GetConflictEventByStaffAsync(
        int staffId,
        DateOnly date,
        TimePeriod period,
        int excludeEventId);

    Task<Event?> GetConflictEventByOutsourceAsync(
        int outsourceId,
        DateOnly date,
        TimePeriod period,
        int excludeEventId);

    void CreateEvent(Event eventEntity);
    void DeleteEvent(Event eventEntity);
    void UpdateEvent(Event eventEntity);
}
