using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Domain.Model
{
    public class EventAssignment
    {
        public int EventAssignmentId { get; set; }
        public int? EventId { get; set; }
        public int? UserId { get; set; }
        public int? RoleId { get; set; }
        public DateTime? AssignedAt { get; set; }
        public int? AssignedByUserId { get; set; }

    }
}
