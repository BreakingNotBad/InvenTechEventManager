using Service.Contracts.DTOs.CompanyContact;
using System.ComponentModel;

namespace Service.Contracts.DTOs.Company
{
    public class CreateCompanyDto
    {
        public required string CompanyName { get; set; }
        public string? Address { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }

        public List<CreateCompanyContactDto> CompanyContacts { get; set; } = [];
    }
}
