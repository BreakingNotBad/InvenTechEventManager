using Contracts.IRepository.BaseManager;
using Entities.Models;
using Service.Contracts.DTOs.Event;
using Service.Contracts.IService;

namespace Service.Service
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

        public async Task CreateEventAsync(CreateEventDto eventDto)
        {
            var newEvent = new Event
            {
                EventName = eventDto.EventName,
                EventType = eventDto.EventType,
                MeetingDate = eventDto.MeetingDate,
                RegistrationTime = eventDto.RegistrationTime,
                StartTime = eventDto.StartTime,
                EndTime = eventDto.EndTime,
                Period = eventDto.Period,
                Latitude = eventDto.Latitude,
                Longitude = eventDto.Longitude,
                Note = eventDto.Note,
                CompanyId = eventDto.CompanyId,
                PackageId = eventDto.PackageId
            };
            _repo.Event.CreateEvent(newEvent);
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
