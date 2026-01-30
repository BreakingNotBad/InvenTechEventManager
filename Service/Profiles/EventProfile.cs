using AutoMapper;
using Entities.Models;
using Presentation.Requests.Event;
using Service.Contracts.DTOs.Event;

public class EventProfile : Profile
{
    public EventProfile()
    {

        CreateMap<Event, EventDto>()
            .ForMember(dest => dest.EventType, opt => opt.MapFrom(src => src.EventType.ToString()))
            .ForMember(dest => dest.EventStaff, opt => opt.MapFrom(src => src.EventStaff.Select(es => es.Staff)))
            .ForMember(dest => dest.EventAttachments, opt => opt.MapFrom(src => src.EventAttachments));

        CreateMap<CreateEventDto, Event>()
             .ForMember(dest => dest.EventAttachments, opt => opt.MapFrom(src => src.Attachments))
             .ForMember(dest => dest.EventStaff, opt => opt.Ignore());

        CreateMap<EventAttachmentDto, EventAttachment>();

        CreateMap<EventAttachment, EventAttachmentDto>();

        CreateMap<EventExtraEquipment, EventExtraEquipmentDto>();

        CreateMap<CreateEventExtraEquipmentDto, EventExtraEquipment>();

        CreateMap<CreateEventOutsourceDto , EventOutsource>();

        CreateMap<EventOutsource, EventOutsourceDto>();



    }
}