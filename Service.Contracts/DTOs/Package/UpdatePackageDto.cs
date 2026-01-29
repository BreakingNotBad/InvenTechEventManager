using Service.Contracts.DTOs.EquipmentSet;
using System.ComponentModel;

namespace Service.Contracts.DTOs.Package
{
    public class UpdatePackageDto
    {
        public string? PackageName { get; set; }

        [DefaultValue(false)]
        public bool? IsDeleted { get; set; }
        public List<UpdateEquipmentSetDto> EquipmentSets { get; set; } = [];
    }
}
