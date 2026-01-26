using Service.Contracts.DTOs.Category;

namespace Service.Contracts.DTOs.Equipment
{
    public class EquipmentDto
    {
        public int EquipmentId { get; set; }
        public string? EquipmentName { get; set; }
        public bool IsDeleted { get; set; }
        public CategoryDto Category { get; set; } = null!;
    }
}
