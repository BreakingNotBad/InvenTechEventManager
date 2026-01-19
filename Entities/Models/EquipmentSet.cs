using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class EquipmentSet
    {

        public int Quantity { get; set; } = 1;

        // Foreign Key: Equipment
        public int EquipmentId { get; set; }

        [ForeignKey(nameof(EquipmentId))]
        public Equipment Equipment { get; set; } = null!;

        // Foreign Key: Package
        public int PackageId { get; set; }

        [ForeignKey(nameof(PackageId))]
        public Package Package { get; set; } = null!;
    }
}
