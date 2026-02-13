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

            // ตรวจสอบ Staff
            foreach (var staff in eventDto.EventStaff)
            {
                var conflictEvent =
                    await _repo.Event.GetConflictEventByStaffAsync( // ตรวจสอบว่าพนักงานมีงานในช่วงเวลานี้หรือไม่
                        staff.StaffId,
                        eventDto.MeetingDate!.Value,
                        eventDto.Period);

                if (conflictEvent != null) // พบ Event ที่ขัดแย้ง
                {
                    //  ลบ Staff ออกจาก Event เก่า
                    var removeItem = conflictEvent.EventStaff
                        .First(x => x.StaffId == staff.StaffId);

                    conflictEvent.EventStaff.Remove(removeItem);
                    conflictEvent.EventStatus =
                        CalculateEventStatus(
                            conflictEvent.RoleRequirements.ToList(),
                            conflictEvent.EventStaff.ToList(),
                            conflictEvent.EventOutsources.ToList());
                    _repo.Event.UpdateEvent(conflictEvent);
                    await _repo.SaveAsync();
                }
            }

            // ตรวจสอบ Outsource
            foreach (var os in eventDto.EventOutsources)
            {
                var conflictEvent =
                    await _repo.Event.GetConflictEventByOutsourceAsync(
                        os.OutsourceId,
                        eventDto.MeetingDate!.Value,
                        eventDto.Period);

                if (conflictEvent != null)
                {
                    //  ลบ Outsource ออกจาก Event เก่า
                    var removeItem = conflictEvent.EventOutsources
                        .First(x => x.OutsourceId == os.OutsourceId);

                    conflictEvent.EventOutsources.Remove(removeItem);
                    conflictEvent.EventStatus =
                        CalculateEventStatus(
                            conflictEvent.RoleRequirements.ToList(),
                            conflictEvent.EventStaff.ToList(),
                            conflictEvent.EventOutsources.ToList());
                    _repo.Event.UpdateEvent(conflictEvent);
                    await _repo.SaveAsync();
                }
            }


            if (eventDto.PackageId == 0)// ถ้า PackageId เป็น 0 ให้ตั้งค่าเป็น null
            {
                eventDto.PackageId = null;
            }

            var eventEntity = _mapper.Map<Event>(eventDto); // map จาก dto ไป entity

            // create EventStaff
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
            // create outsource 
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

            eventEntity.RoleRequirements = eventDto.Requirements
                .Select(r => new EventRoleRequirement
                {
                    RoleId = r.RoleId,
                    Quantity = r.Quantity,
                    SourceType = r.SourceType

                }).ToList();

            eventEntity.EventStatus =
                CalculateEventStatus(
                    eventEntity.RoleRequirements.ToList(),
                    eventEntity.EventStaff.ToList(),
                    eventEntity.EventOutsources.ToList());


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

            if (eventDto.PackageId == 0) // ถ้า PackageId เป็น 0 ให้ตั้งค่าเป็น null
            {
                eventDto.PackageId = null;
            }

            var existingEvent = await _repo.Event.GetEventByIdAsync(id,true);

            // ตรวจสอบ Staff
            foreach (var staff in eventDto.EventStaff)
            {
                var conflictEvent =
                    await _repo.Event.GetConflictEventByStaffAsync(
                        staff.StaffId,
                        existingEvent.MeetingDate,
                        existingEvent.Period,
                        existingEvent.EventId);

                if (conflictEvent != null)
                {
                    var removeItem = conflictEvent.EventStaff
                        .First(x => x.StaffId == staff.StaffId);

                    conflictEvent.EventStaff.Remove(removeItem);
                    conflictEvent.EventStatus =
                        CalculateEventStatus(
                            conflictEvent.RoleRequirements.ToList(),
                            conflictEvent.EventStaff.ToList(),
                            conflictEvent.EventOutsources.ToList());
                    _repo.Event.UpdateEvent(conflictEvent);
                    await _repo.SaveAsync();
                }
            }

            // ตรวจสอบ Outsource
            foreach (var os in eventDto.EventOutsources)
            {
                var conflictEvent =
                    await _repo.Event.GetConflictEventByOutsourceAsync(
                        os.OutsourceId,
                        existingEvent.MeetingDate,
                        existingEvent.Period,
                        existingEvent.EventId);

                if (conflictEvent != null)
                {
                    var removeItem = conflictEvent.EventOutsources
                        .First(x => x.OutsourceId == os.OutsourceId);

                    conflictEvent.EventOutsources.Remove(removeItem);
                    conflictEvent.EventStatus =
                        CalculateEventStatus(
                            conflictEvent.RoleRequirements.ToList(),
                            conflictEvent.EventStaff.ToList(),
                            conflictEvent.EventOutsources.ToList());
                    _repo.Event.UpdateEvent(conflictEvent);
                    await _repo.SaveAsync();
                }
            }

            if (existingEvent == null)
            {
                throw new NotFoundException(nameof(Event), id);
            }
            _mapper.Map(eventDto, existingEvent);

            if (eventDto.DeleteAttachmentIds != null)
            {
                var attachmentsToDelete = existingEvent.EventAttachments
                    .Where(a => eventDto.DeleteAttachmentIds.Contains(a.EventAttachmentId))
                    .ToList();

                foreach (var attachment in attachmentsToDelete)
                {
                    await _fileService.DeleteFileAsync(attachment.FilePath); // ลบจาก storage
                    existingEvent.EventAttachments.Remove(attachment);       // ลบจาก DB
                }
            }

            // Update Requirements
            if (eventDto.Requirements != null)
            {
                var newList = eventDto.Requirements;

                // ลบของเก่าที่ไม่มีอยู่ใน request
                var toRemove = existingEvent.RoleRequirements
                    .Where(old =>
                        !newList.Any(n =>
                            n.RoleId == old.RoleId &&
                            n.SourceType == old.SourceType))
                    .ToList();

                foreach (var item in toRemove)
                    existingEvent.RoleRequirements.Remove(item);

                // Add ใหม่ หรือ Update Quantity
                foreach (var dto in newList)
                {
                    var existing = existingEvent.RoleRequirements
                        .FirstOrDefault(x =>
                            x.RoleId == dto.RoleId &&
                            x.SourceType == dto.SourceType);

                    if (existing != null)
                    {
                        // อัปเดตได้เฉพาะ Quantity
                        existing.Quantity = dto.Quantity;
                    }
                    else
                    {
                        // ต้อง Add ใหม่
                        existingEvent.RoleRequirements.Add(
                            new EventRoleRequirement
                            {
                                RoleId = dto.RoleId,
                                SourceType = dto.SourceType,
                                Quantity = dto.Quantity
                            });
                    }
                }
            }

            // Update Attachment
            if (eventDto.NewAttachments != null)// ถ้ามีการส่งไฟล์มาใหม่
            {
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
            if (eventDto.EventStaff != null)
            {
                var newList = eventDto.EventStaff;

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
            // คำนวณสถานะใหม่
            existingEvent.EventStatus =
                CalculateEventStatus(
                    existingEvent.RoleRequirements.ToList(),
                    existingEvent.EventStaff.ToList(),
                    existingEvent.EventOutsources.ToList());

            // update + save รอบเดียว
            _repo.Event.UpdateEvent(existingEvent);
            await _repo.SaveAsync();

            var eventResponse = _mapper.Map<EventDto>(existingEvent);
            return eventResponse;
        }

        public async Task<AvailabilityResponseDto> CheckAvailabilityAsync(
            CheckAvailabilityRequestDto request)
        {
            if (request.StaffId != null)
            {
                bool available = request.EventId.HasValue
                    ? await _repo.Event.IsStaffAvailableAsync(
                            request.StaffId.Value,
                            request.MeetingDate,
                            request.Period,
                            request.EventId.Value)
                    : await _repo.Event.IsStaffAvailableAsync(
                            request.StaffId.Value,
                            request.MeetingDate,
                            request.Period);

                return new AvailabilityResponseDto
                {
                    IsAvailable = available,
                    Message = available
                        ? "Staff is available"
                        : "Staff already has event in this period"
                };
            }


            if (request.OutsourceId != null)
            {
                bool available = request.EventId.HasValue
                    ? await _repo.Event.IsOutsourceAvailableAsync(
                            request.OutsourceId.Value,
                            request.MeetingDate,
                            request.Period,
                            request.EventId.Value)
                    : await _repo.Event.IsOutsourceAvailableAsync(
                            request.OutsourceId.Value,
                            request.MeetingDate,
                            request.Period);

                return new AvailabilityResponseDto
                {
                    IsAvailable = available,
                    Message = available
                        ? "Outsource is available"
                        : "Outsource already has event in this period"
                };
            }

            throw new ArgumentException("StaffId or OutsourceId is required");
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


        // คำนวณสถานะของงานอีเวนต์
        private EventStatus CalculateEventStatus(
            List<EventRoleRequirement> requirements,
            List<EventStaff> staffs,
            List<EventOutsource> outsources)
        {
            if (requirements == null || !requirements.Any())
                return EventStatus.Pending; 

            foreach (var req in requirements)
            {
                int current = req.SourceType == WorkerSourceType.InternalStaff
                    ? staffs.Count(s => s.RoleId == req.RoleId)
                    : outsources.Count(o => o.RoleId == req.RoleId);

                if (current < req.Quantity)
                    return EventStatus.Pending;
            }

            return EventStatus.Complete;
        }


    }
}
