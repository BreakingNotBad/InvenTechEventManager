using Entity.Domain.Model;

namespace Service.Contract
{
    public interface IEventStaffService
    {
        Task<IEnumerable<EventStaff>> GetEventStaffMembersAsync();
        Task<EventStaff?> GetEventStaffByIdAsync(int id);
    }
}
