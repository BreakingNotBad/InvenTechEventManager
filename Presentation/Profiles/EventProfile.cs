using AutoMapper;
using Presentation.Requests.Event;
using Service.Contracts.DTOs.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Presentation.Profiles
{
    public class EventProfile : Profile
    {
        public EventProfile()
        {
            CreateMap<CreateEventRequest, CreateEventDto>();
            
            CreateMap<CreateEventExtraEquipmentRequest, CreateEventExtraEquipmentDto>();

            CreateMap<CreateEventOutsourceRequest, CreateEventOutsourceDto>();
            //--
            CreateMap<UpdateEventRequest, UpdateEventDto>();

            CreateMap<UpdateEventExtraEquipmentRequest, UpdateEventExtraEquipmentDto>();

            CreateMap<UpdateEventOutsourceRequest, UpdateEventOutsourceDto>();
        }
    }
}
