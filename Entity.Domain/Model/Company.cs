using System.ComponentModel.DataAnnotations;

namespace Entity.Domain.Model
{
    public class Company
    {
        [Key]
        public int CompanyId { get; set; }
        public required string CompanyName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public ICollection<CompanyContact> CompanyContacts { get; set; } = new List<CompanyContact>();
        public ICollection<Event> Events { get; set; } = new List<Event>();
    }
}
