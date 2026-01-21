using AutoMapper;
using Entities.Models;
using Presentation.Requests.Staff;
using Service.Contracts.DTOs.Staff;

namespace Presentation.Mapping
{
    public class StaffProfile : Profile
    {
        public StaffProfile()
        {
            // ========================
            // Request → DTO
            // ========================

            CreateMap<CreateStaffRequest, CreateStaffDto>();

            CreateMap<UpdateStaffRequest, UpdateStaffDto>();

            // ========================
            // DTO → Entity
            // ========================

            CreateMap<CreateStaffDto, Staff>()
                .ForMember(dest => dest.StaffRoles, opt => opt.Ignore())
                .ForMember(dest => dest.Avatar, opt => opt.Ignore());

            CreateMap<UpdateStaffDto, Staff>()
                .ForMember(dest => dest.StaffRoles, opt => opt.Ignore())
                .ForMember(dest => dest.Avatar, opt => opt.Ignore());
        }
    }
}
