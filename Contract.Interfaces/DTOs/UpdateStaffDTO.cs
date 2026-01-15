using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Interfaces.DTOs
{
    public class UpdateStaffDto
    {
        public required string FullName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }

        public bool? IsDeleted { get; set; }

        // ถ้าไม่ส่งมา = ไม่แก้ role
        public List<int>? RoleIds { get; set; }
    }
}
