using Service.Contracts.DTOs.Equipment;
using Service.Contracts.DTOs.EquipmentSet;

namespace Service.Contracts.DTOs.Package
{
    public class PackageDto
    {
        public int PackageId { get; set; }
        public string? PackageName { get; set; }
        public bool IsDeleted { get; set; }
        public ICollection<EquipmentSetDto>? Equipments { get; set; }
    }
}
