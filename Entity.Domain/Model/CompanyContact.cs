using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Domain.Model
{
    public class CompanyContact
    {
        public int CompanyContactId { get; set; }
        public int? CompanyId { get; set; }
        public string? ContactName { get; set; }
        public string? ContactEmail { get; set; }
        public string? ContactPhone { get; set; }
        public string? Role { get; set; }
        public bool? IsPrimary { get; set; }
    }
}
