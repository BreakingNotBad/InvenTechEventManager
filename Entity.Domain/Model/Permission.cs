using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Domain.Model
{
    public class Permission
    {
        public int PermissionId { get; set; }
        public string? PermissionName { get; set; }
        public string? Description { get; set; }
    }
}
