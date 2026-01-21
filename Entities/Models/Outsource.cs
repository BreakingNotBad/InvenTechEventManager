using Entities.Common;

namespace Entities.Models
{
    public class Outsource : BaseEntity
    {
        public int OutsourceId { get; set; }
        public required string FullName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }

        // Navigation Property
        public ICollection<EventOutsource>? EventOutsources { get; set; } = [];
    }
}
