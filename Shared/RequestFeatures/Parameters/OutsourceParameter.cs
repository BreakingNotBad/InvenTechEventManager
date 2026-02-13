using Shared.RequestFeatures.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.RequestFeatures.Parameters
{
    public class OutsourceParameter
    {
        public string? FullName { get; set; }
        public TimePeriod? Period { get; set; }
        public DateOnly? Date { get; set; }
    }
}
