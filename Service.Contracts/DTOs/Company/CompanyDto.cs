using Service.Contracts.DTOs.CompanyContact;

namespace Service.Contracts.DTOs.Company
{
    public class CompanyDto
    {
        public int CompanyId { get; set; }
        public string CompanyName { get; set; } = null!;
        public string? Address { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public List<CompanyContactDto> CompanyContacts { get; set; } = [];
    }
}
