using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Domain.Model
{
    public class EquipmentSets
    {
        [ForeignKey(nameof(Equipments))]
        public int EquipmentId { get; set; }
        public Equipments Equipments { get; set; }
        [ForeignKey(nameof(Packages))]
        public int PackegeId { get; set; }
        public Packages Packages { get; set; }
    }
}
