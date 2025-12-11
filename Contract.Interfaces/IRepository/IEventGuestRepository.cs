using Entity.Domain.Model;

namespace Contract.Interfaces.IRepository
{
    public interface IEventGuestRepository : IRepositoryBase<EventGuest>
    {
        Task<IEnumerable<EventGuest>> GetEventGuestsAsync();
        
        Task<IEnumerable<EventGuest>> GetByEventIdAsync(int eventId);

    }
}
