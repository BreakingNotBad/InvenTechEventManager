using Entities.Common;

namespace Entities.Models
{
    public class Staff : BaseEntity
    {
        public int StaffId { get; set; }
        public required string FullName { get; set; }
        public required string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Avatar { get; set; }

        // Navigation Properties
        public ICollection<StaffPermission> StaffPermissions { get; set; } = [];
        public ICollection<StaffRole> StaffRoles { get; set; } = [];
        public ICollection<EventStaff>? EventStaff { get; set; } = [];
        public ICollection<Event>? CreatedEvents { get; set; } = [];
    }
}
