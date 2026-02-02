using Contracts.IRepository.BaseManager;
using Entities.Models;
using Shared.RequestFeatures.Parameters;

namespace Contracts.IRepository;

public interface IEventRepository : IRepositoryBase<Event>
{
    Task<IEnumerable<Event>> GetEventsAsync(EventParameter eventParameter ,bool trackChanges);
    Task<Event?> GetEventByIdAsync(int id,bool trackChanges);
    void CreateEvent(Event eventEntity);
    void DeleteEvent(Event eventEntity);
    void UpdateEvent(Event eventEntity);
}
