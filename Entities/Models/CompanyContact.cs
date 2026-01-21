using System.ComponentModel.DataAnnotations.Schema;
using Entities.Common;

namespace Entities.Models
{
    public class CompanyContact : AuditableEntity
    {
        public int CompanyContactId { get; set; }
        public required string FullName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Position { get; set; }
        public bool IsPrimary { get; set; } = false;

        // Foreign Key
        public int CompanyId { get; set; }

        [ForeignKey(nameof(CompanyId))]
        public Company? Company { get; set; }
    }
}
