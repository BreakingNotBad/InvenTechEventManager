using AutoMapper;
using Entities.Models;
using Service.Contracts.DTOs.Equipment;
namespace Service.Profiles
{
    public class EquipmentProfile : Profile
    {
        public EquipmentProfile()
        {
            // Create: Map จาก DTO -> Entity
            CreateMap<CreateEquipmentDto, Equipment>();

            // Update: Map จาก DTO -> Entity
            CreateMap<UpdateEquipmentDto, Equipment>();

            // Get: Map จาก Entity -> DTO (Map กลับไปมาได้)
            CreateMap<Equipment, EquipmentDto>()
                .ForMember(
                    dest => dest.Category,
                    opt => opt.MapFrom(src => src.Category)
                );

        }
    }
}
