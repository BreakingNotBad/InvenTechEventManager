using Entities.Common;

namespace Entities.Models
{
    public class Package : BaseEntity
    {
        public int PackageId { get; set; }
        public required string PackageName { get; set; }

        // Navigation Property
        public ICollection<EquipmentSet> EquipmentSets { get; set; } = [];
    }
}
