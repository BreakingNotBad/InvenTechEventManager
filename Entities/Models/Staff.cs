using System.ComponentModel.DataAnnotations;

namespace Entities.Models
{
    public class Staff
    {
        [Key]
        public int StaffId { get; set; }

        [MaxLength(255)]
        public required string FullName { get; set; }

        [MaxLength(255)]
        [EmailAddress]
        public string? Email { get; set; }

        [MaxLength(50)]
        [Phone]
        public string? PhoneNumber { get; set; }

        [MaxLength(1000)]
        public string? Avatar { get; set; }

        public bool IsDeleted { get; set; } = false;

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        // Navigation Properties
        public ICollection<StaffPermission> StaffPermissions { get; set; } =
            new List<StaffPermission>();
        public ICollection<StaffRole> StaffRoles { get; set; } = new List<StaffRole>();
        public ICollection<EventStaff> EventStaff { get; set; } = new List<EventStaff>();
        public ICollection<Event> CreatedEvents { get; set; } = new List<Event>();
    }
}
