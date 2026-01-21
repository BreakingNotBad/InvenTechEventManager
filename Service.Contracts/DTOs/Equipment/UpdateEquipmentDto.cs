namespace Service.Contracts.DTOs.Equipment
{
    public class UpdateEquipmentDto
    {
        public required string EquipmentName { get; set; }
        public int CategoryId { get; set; }
        public bool IsDeleted { get; set; }
    }
}
