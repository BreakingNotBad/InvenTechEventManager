using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Domain.Model
{
    public class StaffRole
    {
        public int StaffId { get; set; }

        [ForeignKey(nameof(StaffId))]
        public Staff Staff { get; set; } = null!;

        public int RoleId { get; set; }

        [ForeignKey(nameof(RoleId))]
        public Role Role { get; set; } = null!;
    }
}
