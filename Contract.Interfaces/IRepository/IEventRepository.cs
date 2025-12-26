using Entity.Domain.Model;

namespace Contract.Interfaces.IRepository;

public interface IEventRepository : IRepositoryBase<Events>
{
    Task<IEnumerable<Events>> GetEventsAsync();
    Task<Events?> GetEventByIdAsync(int id);
    void CreateEvent(Events eventEntity);
    void DeleteEvent(Events eventEntity);
}
