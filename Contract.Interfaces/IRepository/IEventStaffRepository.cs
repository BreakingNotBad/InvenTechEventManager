using Entity.Domain.Model;

namespace Contract.Interfaces.IRepository
{
    public interface IEventStaffRepository : IRepositoryBase<EventStaff>
    {
        Task<IEnumerable<EventStaff>> GetEventStaffMembersAsync();
        Task<EventStaff?> GetEventStaffByIdAsync(int id);
        Task<IEnumerable<EventStaff>> GetEventStaffByEventIdAsync(int eventId);

    }
}
