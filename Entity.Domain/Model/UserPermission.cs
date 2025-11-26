using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Domain.Model
{
    public class UserPermission
    {
        public int UserId { get; set; }
        public int PermissionId { get; set; }
    }
}
