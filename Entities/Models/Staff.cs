namespace Entities.Models
{
    public class Staff
    {
        public int StaffId { get; set; }
        public required string FullName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Avatar { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        // Navigation Properties
        public ICollection<StaffPermission> StaffPermissions { get; set; } = [];
        public ICollection<StaffRole> StaffRoles { get; set; } = [];
        public ICollection<EventStaff>? EventStaff { get; set; } = [];
        public ICollection<Event>? CreatedEvents { get; set; } = [];
    }
}
