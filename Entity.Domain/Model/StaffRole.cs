using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Domain.Model
{
    public class StaffRole
    {
        [ForeignKey(nameof(Staff))]
        public int StaffId { get; set; }
        public Staff Staff { get; set; } = null!;

        [ForeignKey(nameof(RoleName))]
        public int RoleId { get; set; }
        public Role RoleName { get; set; } = null!;
    }
}
