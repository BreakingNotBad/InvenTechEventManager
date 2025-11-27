using Entity.Domain.Model;

namespace Contract.Interfaces.IRepository;

public interface IEventRepository
{
    Task<IEnumerable<Event>> GetAllAsync();
    Task<Event?> GetByIdAsync(int id);
}
