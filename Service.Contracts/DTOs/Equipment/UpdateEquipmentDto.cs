namespace Service.Contracts.DTOs.Equipment
{
    public class UpdateEquipmentDto
    {
        public string? EquipmentName { get; set; }
        public int CategoryId { get; set; }
        public bool IsDeleted { get; set; }
    }
}
