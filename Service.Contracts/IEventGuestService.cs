using Entity.Domain.Model;

namespace Service.Contract
{
    public interface IEventGuestService
    {
        Task<IEnumerable<EventGuest>> GetEventGuestsAsync();
        Task<IEnumerable<EventGuest>> GetByEventIdAsync(int eventId);
    }
}
