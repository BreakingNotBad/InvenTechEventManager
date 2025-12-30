using System.ComponentModel.DataAnnotations;

namespace Entity.Domain.Model
{
    public class Staff
    {
        [Key]
        public int StaffId { get; set; }
        public required string FullName { get; set; }
        public string? Email { get; set; } 
        public string? PhoneNumber { get; set; }
        public byte[]? Avatar { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public ICollection<StaffPermission> StaffPermissions { get; set; } = new List<StaffPermission>();
        public ICollection<StaffRole> StaffRoles { get; set; } = new List<StaffRole>();
        public ICollection<EventStaff> EventStaff { get; set; } = new List<EventStaff>();
        public ICollection<Event> CreatedEvents { get; set; } = new List<Event>();
    }
}
