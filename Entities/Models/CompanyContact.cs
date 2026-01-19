using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class CompanyContact
    {

        public int CompanyContactId { get; set; }


        public required string FullName { get; set; }


        public string? Email { get; set; }


        public string? PhoneNumber { get; set; }

        public string? Position { get; set; }

        public bool IsPrimary { get; set; } = false;

        public bool IsDeleted { get; set; } = false;

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        // Foreign Key
        public int CompanyId { get; set; }

        [ForeignKey(nameof(CompanyId))]
        public Company? Company { get; set; }
    }
}
