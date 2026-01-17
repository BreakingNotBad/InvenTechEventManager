using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class EventOutsource
    {
        public int EventId { get; set; }

        [ForeignKey(nameof(EventId))]
        public Event Event { get; set; } = null!;

        public int OutsourceId { get; set; }

        [ForeignKey(nameof(OutsourceId))]
        public Outsource Outsource { get; set; } = null!;

        public int RoleId { get; set; }

        [ForeignKey(nameof(RoleId))]
        public Role Role { get; set; } = null!;

        public DateTime AssignedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
