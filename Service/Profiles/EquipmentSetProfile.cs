using AutoMapper;
using Entities.Models;
using Service.Contracts.DTOs.EquipmentSet;

namespace Service.Profiles
{
    public class EquipmentSetProfile : Profile
    {
        public EquipmentSetProfile()
        {
            CreateMap<EquipmentSet, EquipmentSetDto>()
                // สั่งให้เอา EquipmentName จากตาราง Equipment
                .ForMember(
                    dest => dest.EquipmentName,
                    opt => opt.MapFrom(src => src.Equipment.EquipmentName)
                )
                // สั่งให้เอา Category จากตาราง Equipment
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Equipment.Category))
                // สั่งให้เอา IsDeleted จากตาราง Equipment
                .ForMember(
                    dest => dest.IsDeleted,
                    opt => opt.MapFrom(src => src.Equipment.IsDeleted)
                );
        }
    }
}
