using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public enum WorkerSourceType
    {
        InternalStaff = 1,
        Outsource = 2
    }
    public class EventRoleRequirement
    {
        public int EventId { get; set; }
        public Event Event { get; set; } = null!;

        public int RoleId { get; set; }
        public Role Role { get; set; } = null!;

        public WorkerSourceType SourceType { get; set; }

        public int Quantity { get; set; }
    }
}
