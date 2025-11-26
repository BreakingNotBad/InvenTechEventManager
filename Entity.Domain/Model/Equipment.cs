using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Domain.Model
{
    public class Equipment
    {
        public int EquipmentId { get; set; }
        public string? EquipmentName { get; set; }
        public string? Description { get; set; }
    }
}
