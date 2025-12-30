using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Domain.Model
{
    public class Equipment
    {
        [Key]
        public int EquipmentId { get; set; }
        public required string EquipmentName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }


        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }
        public Category? Category { get; set; } = null!;
        public ICollection<EquipmentSet> EquipmentSets { get; set; } = new List<EquipmentSet>();
    }
}
