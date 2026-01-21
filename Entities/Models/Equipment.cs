using System.ComponentModel.DataAnnotations.Schema;
using Entities.Common;

namespace Entities.Models
{
    public class Equipment : BaseEntity
    {
        public int EquipmentId { get; set; }
        public required string EquipmentName { get; set; }

        // Foreign Key
        public int CategoryId { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public Category? Category { get; set; }

        // Navigation Property
        public ICollection<EquipmentSet>? EquipmentSets { get; set; } = [];
    }
}
