using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Domain.Model
{
    public class Packages
    {
        [Key]
        public int PackageId { get; set; }
        public string PackageName { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public ICollection<EquipmentSets> EquipmentSets { get; set; } = new List<EquipmentSets>();
    }
}
