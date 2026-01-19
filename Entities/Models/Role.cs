using System.ComponentModel.DataAnnotations;

namespace Entities.Models
{
    public class Role
    {
        public int RoleId { get; set; }
        public required string RoleName { get; set; }

        // Navigation properties
        public ICollection<StaffRole> StaffRoles { get; set; } = [];
        public ICollection<EventOutsource>? EventOutsources { get; set; } = [];
    }
}
