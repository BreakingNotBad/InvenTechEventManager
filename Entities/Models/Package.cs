namespace Entities.Models
{
    public class Package
    {
        public int PackageId { get; set; }
        public required string PackageName { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        // Navigation Property
        public ICollection<EquipmentSet> EquipmentSets { get; set; } = [];
    }
}
