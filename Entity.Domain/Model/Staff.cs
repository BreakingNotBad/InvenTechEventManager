using System.ComponentModel.DataAnnotations;

namespace Entity.Domain.Model
{
    public class Staff
    {
        [Key]
        public int StaffId { get; set; }
        public required string FullName { get; set; }
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public required string[] Roles { get; set; }
        public byte[] Avatar { get; set; } = Array.Empty<byte>();
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public ICollection<StaffPermissions> StaffPermissions { get; set; } = new List<StaffPermissions>();
        public ICollection<Events> CreatedEvents{ get; set; } = new List<Events>();
        public ICollection<EventStaff> EventStaffs { get; set; } = new List<EventStaff>();
    }
}
