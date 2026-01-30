using AutoMapper;
using Contracts.IRepository.BaseManager;
using Entities.Exceptions;
using Entities.Models;
using Service.Contracts.DTOs.Event;
using Service.Contracts.IService;
using Shared.RequestFeatures.Parameters;
namespace Service.Service
{
    public class EventService : IEventService
    {
        private readonly IRepositoryManager _repo;
        private readonly IMapper _mapper;

        public EventService(IRepositoryManager repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EventDto>> GetEventsAsync(EventParameter eventParameter)
        {
            var events = await _repo.Event.GetEventsAsync(eventParameter,false);

            var EventResponse = _mapper.Map<IEnumerable<EventDto>>(events);
            return EventResponse;
        }

        public async Task<EventDto?> GetEventByIdAsync(int id)
        {
            var events = await _repo.Event.GetEventByIdAsync(id);
            if (events == null)
            {
                throw new NotFoundException(nameof(events), id);
            }
            var EventResponse = _mapper.Map<EventDto>(events);
            return EventResponse;
        }

        public async Task<EventDto> CreateEventAsync(CreateEventDto eventDto)
        {
            var eventEntity = _mapper.Map<Event>(eventDto);

            if (eventDto.StaffIds != null && eventDto.StaffIds.Any())
            {
                eventEntity.EventStaff = eventDto.StaffIds
                    .Select(id => new EventStaff { StaffId = id })
                    .ToList();
            }

            if (eventDto.EventOutsources != null && eventDto.EventOutsources.Any())
            {
                eventEntity.EventOutsources = eventDto.EventOutsources
                    .Select(x => new EventOutsource
                    {
                        OutsourceId = x.OutsourceId, 
                        RoleId = x.RoleId            
                    })
                    .ToList();
            }


            if (eventDto.EventExtraEquipments != null && eventDto.EventExtraEquipments.Any())
            {
                eventEntity.EventExtraEquipments = eventDto.EventExtraEquipments
                    .Select(x => new EventExtraEquipment
                    {
                        EquipmentId = x.EquipmentId,
                        Quantity = x.Quantity
                    })
                    .ToList();
            }

            _repo.Event.Create(eventEntity);
            await _repo.SaveAsync();

            var createdEvent = await _repo.Event.GetEventByIdAsync(eventEntity.EventId);

            var eventResponse = _mapper.Map<EventDto>(createdEvent);
            return eventResponse;
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
