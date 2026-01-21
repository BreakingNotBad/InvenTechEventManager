namespace Service.Contracts.DTOs.Equipment
{
    public class CreateEquipmentDto
    {
        public required string EquipmentName { get; set; }
        public int CategoryId { get; set; }
    }
}
