using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Domain.Model
{
    public class CompanyContacts
    {
        [Key]
        public int CompanyContactId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Position { get; set; }   
        public bool IsPrimary { get; set; }

        [ForeignKey(nameof(Companies))]
        public int CompanyId { get; set; }
        public Companies? Company { get; set; }
    }
}
