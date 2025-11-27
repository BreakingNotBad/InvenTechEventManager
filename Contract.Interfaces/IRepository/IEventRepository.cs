using Entity.Domain.Model;

namespace Contact.Interfaces.IRepository;

public interface IEventRepository
{
    Task<IEnumerable<Event>> GetAllAsync();
    Task<Event?> GetByIdAsync(int id);
}
