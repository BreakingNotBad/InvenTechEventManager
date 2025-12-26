using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Domain.Model
{
    public class EventStaff
    {
        [ForeignKey(nameof(Event))]
        public int EventId { get; set; }
        public Events Event { get; set; } = null!;
        [ForeignKey(nameof(Staff))]
        public int StaffId { get; set; }
        public Staff Staff { get; set; } = null!;
        public DateTime AssignedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

    }
}
