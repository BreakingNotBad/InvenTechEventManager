using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Domain.Model
{
    public class EventStaff
    {
        public int EventStaffId { get; set; }
        public int EventId { get; set; }
        public int StaffId { get; set; }
        public DateTime AssignedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

    }
}
