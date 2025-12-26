using System.ComponentModel.DataAnnotations;

namespace Entity.Domain.Model
{
    public class Permission
    {
        [Key]
        public int PermissionId { get; set; }
        public required string PermissionName { get; set; }
        public string? Description { get; set; }

        public ICollection<StaffPermission> StaffPermissions { get; set; } = new List<StaffPermission>();
    }
}
