using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Domain.Model
{
    public class PackageRoleRequirement
    {
        public int PackageId { get; set; }
        public int RoleId { get; set; }
        public int? RequiredQuantity { get; set; }
    }
}
