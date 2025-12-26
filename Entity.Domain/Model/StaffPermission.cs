using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Domain.Model
{
    public class StaffPermission
    {
        [ForeignKey(nameof(Staff))]
        public int StaffId { get; set; }
        public Staff Staff { get; set; } = null!;

        [ForeignKey(nameof(Permission))]
        public int PermissionId { get; set; }
        public Permission Permission { get; set; } = null!;
    }
}
