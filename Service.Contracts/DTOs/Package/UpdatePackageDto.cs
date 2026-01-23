using Service.Contracts.DTOs.EquipmentSet;

namespace Service.Contracts.DTOs.Package
{
    public class UpdatePackageDto
    {
        public string? PackageName { get; set; }
        public List<UpdateEquipmentSetDto> EquipmentSets { get; set; } = [];
    }
}
