using System.ComponentModel;

namespace Service.Contracts.DTOs.Equipment
{
    public class UpdateEquipmentDto
    {
        public string? EquipmentName { get; set; }
        public int CategoryId { get; set; }

        [DefaultValue(false)]
        public bool? IsDeleted { get; set; }
    }
}
