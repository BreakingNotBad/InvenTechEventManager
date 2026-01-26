using Entities.Models;
using Service.Contracts.DTOs.Event;

namespace Service.Contracts.IService
{
    public interface IEventService
    {
        Task<IEnumerable<Event>> GetEventsAsync();
        Task<Event?> GetEventByIdAsync(int id);
        Task CreateEventAsync(CreateEventDto eventDto);
        Task DeleteEvent(int id);
    }
}
