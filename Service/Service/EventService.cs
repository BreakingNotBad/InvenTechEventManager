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
        private readonly IFileService _fileService;

        public EventService(IRepositoryManager repo, IMapper mapper, IFileService fileService)
        {
            _repo = repo;
            _mapper = mapper;
            _fileService = fileService;
        }

        public async Task<IEnumerable<EventDto>> GetEventsAsync(EventParameter eventParameter)
        {
            var events = await _repo.Event.GetEventsAsync(eventParameter,false);

            var EventResponse = _mapper.Map<IEnumerable<EventDto>>(events);
            return EventResponse;
        }

        public async Task<EventDto?> GetEventByIdAsync(int id)
        {
            var events = await _repo.Event.GetEventByIdAsync(id,false);
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
            // create staffevent
            if (eventDto.StaffIds != null && eventDto.StaffIds.Any())
            {
                eventEntity.EventStaff = eventDto.StaffIds
                    .Select(id => new EventStaff { StaffId = id })
                    .ToList();
            }
            // create outsource (Outsource สามารถมีได้หลาย role มั้ย?)
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

            // create extraEquipment
            if (eventDto.EventExtraEquipments != null);
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

            var createdEvent = await _repo.Event.GetEventByIdAsync(eventEntity.EventId,false);

            var eventResponse = _mapper.Map<EventDto>(createdEvent);
            return eventResponse;
        }
        public async Task<EventDto> UpdateEventAsync(int id, UpdateEventDto eventDto)
        {
            var existingEvent = await _repo.Event.GetEventByIdAsync(id,true);

            if (existingEvent == null)
            {
                throw new NotFoundException(nameof(Event), id);
            }
            _mapper.Map(eventDto, existingEvent);

            // Update Attachment
            if(eventDto.NewAttachments != null)
            {
                foreach(var attachment in existingEvent.EventAttachments.ToList())
                {
                    await _fileService.DeleteFileAsync(attachment.FilePath);
                    existingEvent.EventAttachments.Remove(attachment);
                }

                foreach(var dto in eventDto.NewAttachments)
                {
                    existingEvent.EventAttachments.Add(new EventAttachment
                    {
                        OriginalFileName = dto.OriginalFileName,
                        FilePath = dto.FilePath,
                        ContentType = dto.ContentType,
                        FileSize = dto.FileSize,
                    });
                }
            }
            // Update EventStaff
            if (eventDto.StaffIds != null)
            {
                existingEvent.EventStaff.Clear();//<<<ไว้มาแก้ logic ตรงนี้อีกที

                foreach (var staffId in eventDto.StaffIds)
                {
                    existingEvent.EventStaff.Add(new EventStaff
                    {
                        StaffId = staffId
                    });
                }
            }
            // Update EventOutsources (Outsource สามารถมีได้หลาย role มั้ย?)
            if (eventDto.EventOutsources != null)
            {
                foreach(var dto in existingEvent.EventOutsources)
                {
                    var exitsingOutsource = existingEvent.EventOutsources
                        .FirstOrDefault(x => x.OutsourceId == dto.OutsourceId);

                    if(exitsingOutsource != null)
                    {
                        exitsingOutsource.RoleId = dto.RoleId;
                    }
                    else
                    {
                        existingEvent.EventOutsources.Add(new EventOutsource
                        {
                            OutsourceId = dto.OutsourceId,
                            RoleId = dto.RoleId,
                        });
                    }
                }
            }
            // Update ExtraEquipment
            if (eventDto.EventExtraEquipments != null)
            {
                foreach(var dto in existingEvent.EventExtraEquipments)
                {
                    var existingExtraEquipment = existingEvent.EventExtraEquipments
                        .FirstOrDefault(x => x?.EquipmentId == dto.EquipmentId);

                    if(existingExtraEquipment != null)
                    {
                        existingExtraEquipment.Quantity = dto.Quantity;
                    }
                    else
                    {
                        existingEvent.EventExtraEquipments.Add(new EventExtraEquipment
                        {
                            EquipmentId = dto.EquipmentId,
                            Quantity = dto.Quantity,
                        });
                    }
                }
            }
            _repo.Event.UpdateEvent(existingEvent);
            await _repo.SaveAsync();
            var eventResponse = _mapper.Map<EventDto>(existingEvent);
            return eventResponse;
        }
        public async Task DeleteEvent(int id)
        {
            var existingEvent = await _repo.Event.GetEventByIdAsync(id, true);
            if (existingEvent == null)
            {
                throw new ArgumentException($"Event with id {id} not found.");
            }
            _repo.Event.DeleteEvent(existingEvent);
            await _repo.SaveAsync();
        }
    }
}
