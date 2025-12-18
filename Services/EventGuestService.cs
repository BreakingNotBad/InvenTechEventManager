using Contract.Interfaces.IRepository;
using Entity.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Service.Contract;

namespace Service
{
    public class EventGuestService : IEventGuestService
    {
        private readonly IRepositoryManager _repo;

        public EventGuestService(IRepositoryManager repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<EventGuest>> GetEventGuestsAsync()
        {
            return await _repo.EventGuest.GetEventGuestsAsync();
        }

        public async Task<IEnumerable<EventGuest>> GetByEventIdAsync(int eventId)
        {
            return await _repo.EventGuest.GetByEventIdAsync(eventId);
        }
    }
}
