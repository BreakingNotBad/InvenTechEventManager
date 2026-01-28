using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.RequestFeatures.Parameters
{
    public class EquipmentParameter
    {
        public string? EquipmentName { get; set; }
        public string? Category { get; set; } // Search by Category Name
        public bool? IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
