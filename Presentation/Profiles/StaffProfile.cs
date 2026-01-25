using AutoMapper;
using Presentation.Requests.Staff;
using Service.Contracts.DTOs.Staff;

namespace Presentation.Profiles
{
    public class StaffProfile : Profile
    {
        public StaffProfile()
        {
            // Create: Map จาก Request  -> DTO
            CreateMap<CreateStaffRequest, CreateStaffDto>();

            // Update: Map จาก Request  -> DTO
            CreateMap<UpdateStaffRequest, UpdateStaffDto>();
        }
    }
}
