using Service.Contracts.DTOs.EquipmentSet;

namespace Service.Contracts.DTOs.Package
{
    public class UpdatePackageDto
    {
        public required string PackageName { get; set; }
        public List<UpdateEquipmentSetDto> EquipmentSets { get; set; } = [];
    }
}
