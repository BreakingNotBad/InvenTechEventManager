namespace Entity.Domain.Model
{
    public class Equipments
    {
        public int EquipmentId { get; set; }
        public string EquipmentName { get; set; } = string.Empty;
        public string Categeory { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public ICollection<EquipmentSets> EquipmentSets { get; set; } = new List<EquipmentSets>();
    }
}
