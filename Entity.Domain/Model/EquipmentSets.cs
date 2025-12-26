namespace Entity.Domain.Model
{
    public class EquipmentSets
    {
        public int EquipmentSetId { get; set; }
        public string PackegeId { get; set; }
        public ICollection<Equipments> Equipments { get; set; } = new List<Equipments>();
        public ICollection<Packages> Packages { get; set; } = new List<Packages>();
    }
}
