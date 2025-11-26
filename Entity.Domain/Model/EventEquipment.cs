using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Domain.Model
{
    public class EventEquipment
    {
        public int EventId { get; set; }
        public int EquipmentId { get; set; }
        public int? Quantity { get; set; }
    }
}
