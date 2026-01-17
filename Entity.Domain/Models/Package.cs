using System.ComponentModel.DataAnnotations;

namespace Entities.Models
{
    public class Package
    {
        [Key]
        public int PackageId { get; set; }

        [MaxLength(100)]
        public required string PackageName { get; set; }

        public bool IsDeleted { get; set; } = false;

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        // Navigation Property
        public ICollection<EquipmentSet> EquipmentSets { get; set; } = new List<EquipmentSet>();
    }
}
