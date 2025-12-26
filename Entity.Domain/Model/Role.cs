
using System.ComponentModel.DataAnnotations;

namespace Entity.Domain.Model
{
    public class Role
    {
        [Key]
        public int RoleId { get; set; }
        public required string RoleName { get; set; }

        public ICollection<StaffRole> StaffRoles { get; set; } = new List<StaffRole>();
        public ICollection<EventOutsource> EventOutsources { get; set; } = new List<EventOutsource>();
    }
}
