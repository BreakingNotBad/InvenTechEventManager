using Service.Contracts.DTOs.CompanyContact;
using System.ComponentModel;

namespace Service.Contracts.DTOs.Company
{
    public class UpdateCompanyDto
    {
        public string? CompanyName { get; set; }
        public string? CompanyShortName { get; set; }
        public string? Address { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }

        [DefaultValue(false)]
        public bool? IsDeleted { get; set; }

        public List<UpdateCompanyContactDto> CompanyContacts { get; set; } = [];
    }
}
