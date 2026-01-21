using Service.Contracts.DTOs.EquipmentSet;

namespace Service.Contracts.DTOs.Package
{
    public class CreatePackageDto
    {
        public required string PackageName { get; set; }
        public List<CreateEquipmentSetDto> EquipmentSets { get; set; } = [];
    }
}
