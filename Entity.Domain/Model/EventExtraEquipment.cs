using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Domain.Model
{
    public class EventExtraEquipment
    {
        [ForeignKey(nameof(Event))]
        public int EventId { get; set; }
        public Event Event { get; set; } = null!;

        [ForeignKey(nameof(Equipment))]
        public int EquipmentId { get; set; }
        public Equipment Equipment { get; set; } = null!;

        public int Quantity { get; set; }
    }
}
