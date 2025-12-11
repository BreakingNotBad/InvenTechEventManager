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
            return await _repo.EventGuest.FindByCondition(
                e => e.EventId == eventId,
                trackChanges: false
            ).ToListAsync();
        }

        public async Task<EventGuest> CreateEventGuestAsync(EventGuest entity)
        {
            _repo.EventGuest.Create(entity);
            await _repo.SaveAsync();
            return entity;
        }

        public async Task<bool> DeleteEventGuestAsync(int eventId, int guestUserId)
        {
            var existing = await _repo.EventGuest.FindByCondition(
                e => e.EventId == eventId && e.GuestUserId == guestUserId,
                trackChanges: true
            ).FirstOrDefaultAsync();

            if (existing == null)
            {
                return false;
            }

            _repo.EventGuest.Delete(existing);
            await _repo.SaveAsync();
            return true;
        }
    }
}
