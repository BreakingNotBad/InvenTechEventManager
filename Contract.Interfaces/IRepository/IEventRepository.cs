using Entity.Domain.Model;

namespace Contract.Interfaces.IRepository;

public interface IEventRepository
{
    Task<IEnumerable<Event>> GetEventsAsync();
    Task<Event?> GetEventByIdAsync(int id);
}
