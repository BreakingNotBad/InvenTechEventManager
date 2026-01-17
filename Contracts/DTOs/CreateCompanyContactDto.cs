using System.ComponentModel;
using System.Text.Json.Serialization;

namespace Contracts.DTOs
{
    public class CreateCompanyContactDto
    {
        public int? CompanyContactId { get; set; }
        public string FullName { get; set; } = null!;
        public string? Position { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }

        [DefaultValue(false)]
        public bool IsPrimary { get; set; }

        [DefaultValue(false)]
        public bool IsDeleted { get; set; }
    }
}
