using AutoMapper;
using Entities.Models;
using Service.Contracts.DTOs.Package;
namespace Service.Profiles
{
    public class PackageProfile : Profile
    {
        public PackageProfile()
        {   // Create: Map จาก DTO -> Entity
            CreateMap<CreatePackageDto, Package>()
                .ForMember(
                    dest => dest.EquipmentSets,
                    opt => opt.Ignore()
                );

            // Update: Map จาก DTO -> Entity
            CreateMap<UpdatePackageDto, Package>()
                .ForMember(
                    dest => dest.EquipmentSets,
                    opt => opt.Ignore()
                );

            // Get: Map จาก Entity -> DTO
            CreateMap<Package, PackageDto>()
                                .ForMember(
                    dest => dest.Equipments,
                    opt =>
                        opt.MapFrom(src => src.EquipmentSets.Select(sr => sr.Equipment).ToList())
                );
        }
    }
}
