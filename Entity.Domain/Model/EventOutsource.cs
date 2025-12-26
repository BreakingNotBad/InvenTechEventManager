using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Domain.Model
{
    public class EventOutsource
    {
        [ForeignKey(nameof(Event))]
        public int EventId { get; set; }
        public Event Event { get; set; } = null!;

        [ForeignKey(nameof(Outsource))]
        public int OutsourceId { get; set; }
        public Outsource Outsource { get; set; } = null!;

        [ForeignKey(nameof(RoleName))]
        public int RoleId { get; set; }
        public Role RoleName { get; set; } = null!;

        public DateTime AssignedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
