using AutoMapper;
using Entities.Models;
using Service.Contracts.DTOs.Company;

namespace Service.Profiles
{
    public class CompanyProfile : Profile
    {
        public CompanyProfile()
        {
            // Create: Map จาก DTO -> Entity
            CreateMap<CreateCompanyDto, Company>();

            // Update: Map จาก DTO -> Entity
            CreateMap<UpdateCompanyDto, Company>()
                .ForMember(c => c.CompanyContacts, opt => opt.Ignore());

            // Get: Map จาก Entity -> DTO (Map กลับไปมาได้)
            CreateMap<Company, CompanyDto>().ReverseMap();
        }
    }
}
