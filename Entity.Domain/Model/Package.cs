using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Domain.Model
{
    public class Package
    {
        public int PackageId { get; set; }
        public string PackageName { get; set; } = string.Empty;
        public string? Description { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? Price { get; set; }
        public int? EquipmentSetId { get; set; }
    }
}
