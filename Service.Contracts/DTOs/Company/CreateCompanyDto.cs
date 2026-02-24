using Service.Contracts.DTOs.CompanyContact;

namespace Service.Contracts.DTOs.Company
{
    public class CreateCompanyDto
    {
        public string? CompanyName { get; set; }
        public string? CompanyShortName { get; set; }
        public string? Address { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }

        public List<CreateCompanyContactDto> CompanyContacts { get; set; } = [];
    }
}
