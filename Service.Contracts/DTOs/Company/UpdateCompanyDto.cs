using Service.Contracts.DTOs.CompanyContact;

namespace Service.Contracts.DTOs.Company
{
    public class UpdateCompanyDto
    {
        public string? CompanyName { get; set; }
        public string? Address { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public bool IsDeleted { get; set; }

        public List<UpdateCompanyContactDto> CompanyContacts { get; set; } = [];
    }
}
