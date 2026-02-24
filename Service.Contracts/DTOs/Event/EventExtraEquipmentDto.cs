using Service.Contracts.DTOs.Equipment;

namespace Service.Contracts.DTOs.Event
{
    public class EventExtraEquipmentDto
    {
        public int Quantity { get; set; }        
        public string? Remark { get; set; }
        public EquipmentDto Equipment { get; set; } = null!;
    }
}