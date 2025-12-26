using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Domain.Model
{
    public class EquipmentSet
    {
        [ForeignKey(nameof(Equipment))]
        public int EquipmentId { get; set; }
        public Equipment Equipment { get; set; } = null!;

        [ForeignKey(nameof(Package))]
        public int PackageId { get; set; }
        public Package Package { get; set; } = null!;
    }
}
