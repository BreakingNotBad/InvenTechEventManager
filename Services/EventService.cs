using Contract.Interfaces.IRepository;
using Entity.Domain.Model;
using Service.Contract;
using Microsoft.EntityFrameworkCore;

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
        public async Task CreateEventAsync(Event eventEntity)
        {
            _repo.Event.CreateEvent(eventEntity);
            await _repo.SaveAsync();
        }
        public async Task DeleteEvent(int id)
        {
            var existingEvent = await _repo.Event.GetEventByIdAsync(id);
            if (existingEvent == null)
            {
                throw new ArgumentException($"Event with id {id} not found.");
            }
            _repo.Event.DeleteEvent(existingEvent);
            await _repo.SaveAsync();
        }
    }
}
