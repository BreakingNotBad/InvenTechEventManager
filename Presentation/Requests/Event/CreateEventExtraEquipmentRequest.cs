using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Requests.Event
{
    public class CreateEventExtraEquipmentRequest
    {
        public int EquipmentId { get; set; }
        public int Quantity { get; set; }
        public string? Remark { get; set; }
    }
}
