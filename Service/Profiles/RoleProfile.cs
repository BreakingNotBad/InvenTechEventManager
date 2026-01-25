using AutoMapper;
using Entities.Models;
using Service.Contracts.DTOs.Role;

namespace Service.Profiles
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            //  Get: Map จาก Entity -> DTO
            CreateMap<Role, RoleDto>();
        }
    }
}
