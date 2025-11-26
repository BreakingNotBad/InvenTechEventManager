using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Domain.Model
{
    public class EventGuest
    {
        public int EventId { get; set; }
        public int GuestUserId { get; set; }
    }
}
