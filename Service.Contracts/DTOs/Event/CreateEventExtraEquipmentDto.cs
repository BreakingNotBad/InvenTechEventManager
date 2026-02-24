using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts.DTOs.Event
{
    public class CreateEventExtraEquipmentDto
    {
        public int EquipmentId { get; set; }
        public int Quantity { get; set; }
        public string? Remark { get; set; }
    }
}
