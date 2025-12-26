using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Domain.Model
{
    public class StaffPermissions
    {
        [ForeignKey(nameof(Staff))]
        public int StaffId { get; set; }
        public Staff Staff { get; set; } = null!;
        [ForeignKey(nameof(Permissions))]
        public int PermissionId { get; set; }
        public Permissions Permissions { get; set; } = null!;
    }
}
