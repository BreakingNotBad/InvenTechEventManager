using Entity.Domain.Model;

namespace Service.Contract
{
    public interface IEventService
    {
        Task<IEnumerable<Event>> GetEventsAsync();
        Task<Event?> GetEventByIdAsync(int id);
    }
}
