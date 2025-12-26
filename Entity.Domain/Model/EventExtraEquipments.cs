using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Domain.Model
{
    public class EventExtraEquipments
    {
        [ForeignKey(nameof(Event))]
        public int EventId { get; set; }
        public Event Event { get; set; }
        [ForeignKey(nameof(Equipments))]
        public int EquipmentId { get; set; }
        public Equipments Equipments { get; set; }
        public int Quantity { get; set; }
    }
}
