using AutoMapper;
using Entities.Models;
using Service.Contracts.DTOs.CompanyContact;

namespace Service.Profiles
{
    public class CompanyContactProfile : Profile
    {
        public CompanyContactProfile()
        {
            // Create: Map จาก DTO ลูก -> Entity ลูก
            CreateMap<CreateCompanyContactDto, CompanyContact>();

            // Update: Map จาก DTO ลูก -> Entity ลูก
            CreateMap<UpdateCompanyContactDto, CompanyContact>();

            // Get: Map จาก Entity ลูก -> DTO ลูก
            CreateMap<CompanyContact, CompanyContactDto>().ReverseMap();
        }
    }
}
