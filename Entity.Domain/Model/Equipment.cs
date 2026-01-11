using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Domain.Model
{
    public class Equipment
    {
        [Key]
        public int EquipmentId { get; set; }

        [MaxLength(100)]
        public required string EquipmentName { get; set; }

        public bool IsDeleted { get; set; } = false;

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        // Foreign Key
        public int CategoryId { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public Category? Category { get; set; }

        public ICollection<EquipmentSet> EquipmentSets { get; set; } = new List<EquipmentSet>();
    }
}
