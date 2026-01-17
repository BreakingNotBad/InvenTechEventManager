using Contracts.IRepository.BaseManager;
using Entities.Models;

namespace Contracts.IRepository;

public interface IEventRepository : IRepositoryBase<Event>
{
    Task<IEnumerable<Event>> GetEventsAsync();
    Task<Event?> GetEventByIdAsync(int id);
    void CreateEvent(Event eventEntity);
    void DeleteEvent(Event eventEntity);
}
