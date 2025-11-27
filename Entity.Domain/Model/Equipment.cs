namespace Entity.Domain.Model
{
    public class Equipment
    {
        public int EquipmentId { get; set; }
        public string EquipmentName { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}
