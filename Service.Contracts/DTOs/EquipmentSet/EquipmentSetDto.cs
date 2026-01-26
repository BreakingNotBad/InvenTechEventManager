using Service.Contracts.DTOs.Category;

namespace Service.Contracts.DTOs.EquipmentSet
{
    public class EquipmentSetDto
    {
        public int EquipmentId { get; set; }
        public string? EquipmentName { get; set; }
        public int Quantity { get; set; }
        public bool IsDeleted { get; set; }
        public CategoryDto Category { get; set; } = null!;

    }
}
