using AutoMapper;
using Contracts.IRepository.BaseManager;
using Entities.Exceptions;
using Entities.Models;
using FluentValidation;
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
        private readonly IValidator<CreateEventDto> _createValidator;
        private readonly IValidator<UpdateEventDto> _updateValidator;

        public EventService(
            IRepositoryManager repo, 
            IMapper mapper, 
            IFileService fileService,
            IValidator<CreateEventDto> createValidator,
            IValidator<UpdateEventDto> updateValidator)
        {
            _repo = repo;
            _mapper = mapper;
            _fileService = fileService;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
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
        // Create Event
        public async Task<EventDto> CreateEventAsync(CreateEventDto eventDto)
        {
            await _createValidator.ValidateAndThrowAsync(eventDto);

            var eventEntity = _mapper.Map<Event>(eventDto); // map จาก dto ไป entity
                                                            // create staffevent
            if (eventDto.EventStaff != null)
            {
                eventEntity.EventStaff = eventDto.EventStaff
                    .Select(x => new EventStaff
                    {
                        StaffId = x.StaffId,
                        RoleId = x.RoleId
                    })
                    .ToList();
            }
            // create outsource (Outsource สามารถมีได้หลาย role มั้ย?)
            if (eventDto.EventOutsources != null)
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
            if (eventDto.EventExtraEquipments != null)
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
        // Update Event
        public async Task<EventDto> UpdateEventAsync(int id, UpdateEventDto eventDto)
        {
            await _updateValidator.ValidateAndThrowAsync(eventDto);

            var existingEvent = await _repo.Event.GetEventByIdAsync(id,true);

            if (existingEvent == null)
            {
                throw new NotFoundException(nameof(Event), id);
            }
            _mapper.Map(eventDto, existingEvent);

            // Update Attachment
            if(eventDto.NewAttachments != null)// ถ้ามีการส่งไฟล์มาใหม่
            {
                foreach(var attachment in existingEvent.EventAttachments.ToList())// ลบไฟล์เก่า
                {
                    await _fileService.DeleteFileAsync(attachment.FilePath);// ลบไฟล์จาก storage
                    existingEvent.EventAttachments.Remove(attachment);// ลบไฟล์จาก database
                }

                foreach(var dto in eventDto.NewAttachments)// เพิ่มไฟล์ใหม่
                {
                    existingEvent.EventAttachments.Add(new EventAttachment// เพิ่มไฟล์ใหม่ ใน database
                    {
                        OriginalFileName = dto.OriginalFileName,
                        FilePath = dto.FilePath,
                        ContentType = dto.ContentType,
                        FileSize = dto.FileSize,
                    });
                }
            }
            // Update EventStaff
            if (eventDto.EventStaffs != null)
            {
                var newList = eventDto.EventStaffs;

                // ADD or UPDATE
                foreach (var dto in newList)
                {
                    var existing = existingEvent.EventStaff
                        .FirstOrDefault(x => x.StaffId == dto.StaffId);

                    if (existing != null)
                    {
                        existing.RoleId = dto.RoleId;
                    }
                    else
                    {
                        existingEvent.EventStaff.Add(new EventStaff
                        {
                            StaffId = dto.StaffId,
                            RoleId = dto.RoleId
                        });
                    }
                }

                // REMOVE
                var toRemove = existingEvent.EventStaff
                    .Where(x => !newList.Any(n => n.StaffId == x.StaffId))
                    .ToList();

                foreach (var item in toRemove)
                    existingEvent.EventStaff.Remove(item);
            }
            // Update EventOutsources
            if (eventDto.EventOutsources != null)
            {
                var newList = eventDto.EventOutsources;

                // ADD or UPDATE
                foreach (var dto in newList)
                {
                    var existing = existingEvent.EventOutsources
                        .FirstOrDefault(x => x.OutsourceId == dto.OutsourceId);

                    if (existing != null)
                    {
                        existing.RoleId = dto.RoleId;
                    }
                    else
                    {
                        existingEvent.EventOutsources.Add(new EventOutsource
                        {
                            OutsourceId = dto.OutsourceId,
                            RoleId = dto.RoleId
                        });
                    }
                }

                // REMOVE
                var toRemove = existingEvent.EventOutsources
                    .Where(x => !newList.Any(n => n.OutsourceId == x.OutsourceId))
                    .ToList();

                foreach (var item in toRemove)
                    existingEvent.EventOutsources.Remove(item);
            }
            // Update ExtraEquipment
            if (eventDto.EventExtraEquipments != null)
            {
                var newList = eventDto.EventExtraEquipments;

                // ADD or UPDATE
                foreach (var dto in newList)
                {
                    var existing = existingEvent.EventExtraEquipments
                        .FirstOrDefault(x => x.EquipmentId == dto.EquipmentId);

                    if (existing != null)
                    {
                        existing.Quantity = dto.Quantity;
                    }
                    else
                    {
                        existingEvent.EventExtraEquipments.Add(new EventExtraEquipment
                        {
                            EquipmentId = dto.EquipmentId,
                            Quantity = dto.Quantity
                        });
                    }
                }

                // REMOVE
                var toRemove = existingEvent.EventExtraEquipments
                    .Where(x => !newList.Any(n => n.EquipmentId == x.EquipmentId))
                    .ToList();

                foreach (var item in toRemove)
                    existingEvent.EventExtraEquipments.Remove(item);
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
