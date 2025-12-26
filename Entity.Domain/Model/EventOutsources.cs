using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Domain.Model
{
    public class EventOutsources
    {
        [ForeignKey(nameof(Event))]
        public int EventId { get; set; }
        public Event Event { get; set; } = null!;
        [ForeignKey(nameof(Outsources))]
        public int OutsourcesId { get; set; }
        public Outsources Outsources { get; set; } = null!;
        public string Roles { get; set; } = string.Empty;
        public DateTime AssignedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
