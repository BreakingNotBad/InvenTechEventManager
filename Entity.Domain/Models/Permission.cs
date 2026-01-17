using System.ComponentModel.DataAnnotations;

namespace Entities.Models
{
    public class Permission
    {
        [Key]
        public int PermissionId { get; set; }

        [MaxLength(100)]
        public required string PermissionName { get; set; }

        [MaxLength(500)]
        public string? Description { get; set; }

        public ICollection<StaffPermission> StaffPermissions { get; set; } =
            new List<StaffPermission>();
    }
}
