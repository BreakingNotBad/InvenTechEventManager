using Contract.Interfaces.IRepository;
using Entity.Domain.Model;
using Service.Contract;

namespace Service
{
    public class EventService : IEventService
    {
        private readonly IRepositoryManager _repo;
        public EventService(IRepositoryManager repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<Event>> GetEventsAsync()
        {
            return await _repo.Event.GetEventsAsync();
        }

        public async Task<Event?> GetEventByIdAsync(int id)
        {
            return await _repo.Event.GetEventByIdAsync(id);
        }
    }
}
