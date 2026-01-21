using System.ComponentModel.DataAnnotations.Schema;
using Entities.Common;

namespace Entities.Models
{
    public class EventOutsource : AuditableEntity
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
    }
}
