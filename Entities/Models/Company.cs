namespace Entities.Models
{
    public class Company
    {
        public int CompanyId { get; set; }
        public required string CompanyName { get; set; }
        public string? Address { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        // Navigation Properties
        public ICollection<CompanyContact> CompanyContacts { get; set; } = [];
        public ICollection<Event>? Events { get; set; } = [];
    }
}
