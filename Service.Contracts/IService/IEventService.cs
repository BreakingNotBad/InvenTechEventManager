using Entities.Models;
using Service.Contracts.DTOs.Event;
using Shared.RequestFeatures.Parameters;

namespace Service.Contracts.IService
{
    public interface IEventService
    {
        Task<IEnumerable<EventDto>> GetEventsAsync(EventParameter eventParameter);
        Task<EventDto?> GetEventByIdAsync(int id);
        Task <EventDto>CreateEventAsync(CreateEventDto eventDto);
        Task <EventDto>UpdateEventAsync(int id, UpdateEventDto eventDto);
        Task DeleteEvent(int id);

        //Task<AvailabilityResponseDto> CheckAvailabilityAsync(
        //    CheckAvailabilityRequestDto request);



    }
}
