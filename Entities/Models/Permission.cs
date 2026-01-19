using System.ComponentModel.DataAnnotations;

namespace Entities.Models
{
    public class Permission
    {

        public int PermissionId { get; set; }


        public required string PermissionName { get; set; }


        public string? Description { get; set; }

        public ICollection<StaffPermission> StaffPermissions { get; set; } =
            new List<StaffPermission>();
    }
}
