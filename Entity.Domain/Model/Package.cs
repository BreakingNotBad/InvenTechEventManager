using System.ComponentModel.DataAnnotations;

namespace Entity.Domain.Model
{
    public class Package
    {
        [Key]
        public int PackageId { get; set; }
        public required string PackageName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public ICollection<EquipmentSet> EquipmentSets { get; set; } = new List<EquipmentSet>();
    }
}
