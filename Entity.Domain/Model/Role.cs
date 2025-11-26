using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Domain.Model
{
    public class Role
    {
        public int RoleId { get; set; }
        public string? RoleName { get; set; }
        public string? Description { get; set; }
    }
}
