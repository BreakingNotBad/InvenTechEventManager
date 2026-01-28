using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.RequestFeatures.Parameters
{
    public class StaffParameter
    {
        public string? FullName { get; set; }
        public string? Status { get; set; } // active, inactive
        public string? Role { get; set; }
        public bool? Available { get; set; }
        public DateTime? Date { get; set; }
        public string? Period { get; set; } // day, week, month
    }
}
