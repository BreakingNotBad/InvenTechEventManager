using Entities.Models;

namespace Service.Contracts.IService
{
    public interface IEventService
    {
        Task<IEnumerable<Event>> GetEventsAsync();
        Task<Event?> GetEventByIdAsync(int id);
        Task CreateEventAsync(Event eventEntity);
        Task DeleteEvent(int id);
    }
}
