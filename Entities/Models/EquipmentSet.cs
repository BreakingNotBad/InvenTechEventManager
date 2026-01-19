using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class EquipmentSet
    {
        public int EquipmentId { get; set; }

        [ForeignKey(nameof(EquipmentId))]
        public Equipment Equipment { get; set; } = null!;

        public int PackageId { get; set; }

        [ForeignKey(nameof(PackageId))]
        public Package Package { get; set; } = null!;

        public int Quantity { get; set; } = 1;
    }
}
