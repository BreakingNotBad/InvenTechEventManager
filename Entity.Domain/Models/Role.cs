using System.ComponentModel.DataAnnotations;

namespace Entities.Models
{
    public class Role
    {
        [Key]
        public int RoleId { get; set; }

        [MaxLength(100)]
        public required string RoleName { get; set; }

        public ICollection<StaffRole> StaffRoles { get; set; } = new List<StaffRole>();
        public ICollection<EventOutsource> EventOutsources { get; set; } =
            new List<EventOutsource>();
    }
}
