using Entity.Domain.Model;

namespace Service.Contract
{
    public interface IEventService
    {
        Task<IEnumerable<Events>> GetEventsAsync();
        Task<Events?> GetEventByIdAsync(int id);
        Task CreateEventAsync(Events eventEntity);
        Task DeleteEvent(int id);
    }
}
