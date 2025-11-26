using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Domain.Model
{
    public class Company
    {
        public int CompanyId { get; set; }
        public string? CompanyName { get; set; }
        public int? PrimaryContactId { get; set; }
    }
}
