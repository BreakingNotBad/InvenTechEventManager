using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Domain.Model
{
    public class Company
    {
        [Key]
        public int CompanyId { get; set; }

        [MaxLength(255)]
        public required string CompanyName { get; set; }

        [MaxLength(500)]
        public string Address { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18, 15)")]
        public decimal? Latitude { get; set; }

        [Column(TypeName = "decimal(18, 15)")]
        public decimal? Longitude { get; set; }

        public bool IsDeleted { get; set; } = false;

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        // Navigation Properties
        public ICollection<CompanyContact> CompanyContacts { get; set; } =
            new List<CompanyContact>();
        public ICollection<Event> Events { get; set; } = new List<Event>();
    }
}
