using Shared.RequestFeatures.Enums;

namespace Shared.RequestFeatures.Parameters
{
    public class PackageParameter
    {
        public string? PackageName { get; set; }
        public EquipmentStatusEnum EquipmentStatus { get; set; } = EquipmentStatusEnum.All;
    }
}
