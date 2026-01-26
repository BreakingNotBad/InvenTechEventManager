using AutoMapper;
using Entities.Models;
using Service.Contracts.DTOs.Outsource;
namespace Service.Profiles
{
    public class OutsourceProfile : Profile
    {
        public OutsourceProfile()
        {   // Create: Map จาก DTO -> Entity
            CreateMap<CreateOutsourceDto, Outsource>();

            // Update: Map จาก DTO -> Entity
            CreateMap<UpdateOutsourceDto, Outsource>();

            // Get: Map จาก Entity -> DTO (Map กลับไปมาได้)
            CreateMap<Outsource, OutsourceDto>().ReverseMap();
        }
    }
}
