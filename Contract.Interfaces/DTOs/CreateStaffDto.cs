using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Interfaces.DTOs
{
    public class CreateStaffRequest
    {
        public required string FullName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }

        // รับเป็น List ของ Int ตาม JSON
        public List<int> RoleIds { get; set; } = new List<int>();
    }
}
