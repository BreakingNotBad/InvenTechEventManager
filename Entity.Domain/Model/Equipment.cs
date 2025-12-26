using System.ComponentModel.DataAnnotations;

namespace Entity.Domain.Model
{
    public class Equipment
    {
        [Key]
        public int EquipmentId { get; set; }
        public required string EquipmentName { get; set; }
        public required string Category { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public ICollection<EquipmentSet> EquipmentSets { get; set; } = new List<EquipmentSet>();
    }
}
