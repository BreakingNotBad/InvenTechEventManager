using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Domain.Model
{
    public class EventStaff
    {
        public int EventId { get; set; }

        [ForeignKey(nameof(EventId))]
        public Event Event { get; set; } = null!;

        public int StaffId { get; set; }

        [ForeignKey(nameof(StaffId))]
        public Staff Staff { get; set; } = null!;

        public DateTime AssignedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
