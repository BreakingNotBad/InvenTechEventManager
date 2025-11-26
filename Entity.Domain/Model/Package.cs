using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Domain.Model
{
    public class Package
    {
        public int PackageId { get; set; }
        public string? PackageName { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public int? EquipmentSetId { get; set; }    
    }
}
