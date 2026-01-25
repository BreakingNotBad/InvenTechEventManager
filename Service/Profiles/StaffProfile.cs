using AutoMapper;
using Entities.Models;
using Service.Contracts.DTOs.Staff;

namespace Service.Profiles
{
    public class StaffProfile : Profile
    {
        public StaffProfile()
        {
            // Create: Map จาก DTO -> Entity
            CreateMap<CreateStaffDto, Staff>()
                .ForMember(dest => dest.StaffRoles, opt => opt.Ignore())
                .ForMember(dest => dest.Avatar, opt => opt.Ignore());

            // Update: Map จาก DTO -> Entity
            CreateMap<UpdateStaffDto, Staff>()
                .ForMember(dest => dest.StaffRoles, opt => opt.Ignore())
                .ForMember(dest => dest.Avatar, opt => opt.Ignore());

            // Get: Map จาก Entity -> DTO
            CreateMap<Staff, StaffDto>()
                // Map: StaffRoles (List<StaffRole>) ---> Roles (List<RoleDto>)
                .ForMember(
                    dest => dest.Roles,
                    opt =>
                        // 1. src.StaffRoles: เข้าไปที่ตารางกลาง
                        // 2. .Select(sr => sr.Role): เลือกเอาเฉพาะตัว "Role" ออกมา
                        // 3. AutoMapper จะรู้งานเองว่าต้องแปลง Role -> RoleDto (ตามข้อ 1 ที่เราทำไว้)
                        opt.MapFrom(src => src.StaffRoles.Select(sr => sr.Role).ToList())
                );
        }
    }
}
