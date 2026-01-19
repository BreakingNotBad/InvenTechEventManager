using System.ComponentModel;

namespace Contracts.DTOs
{
    public class UpdateCompanyDto
    {
        public string CompanyName { get; set; } = null!;
        public string? Address { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }

        [DefaultValue(false)]
        public bool IsDeleted { get; set; }

        public List<UpdateCompanyContactDto> CompanyContacts { get; set; } = [];
    }
}
