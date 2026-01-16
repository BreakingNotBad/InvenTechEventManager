using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Requests.Staff
{
    public class UpdateStaffRequest
    {
        public string FullName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public IFormFile? AvatarFile { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? DeleteAvatar { get; set; }

        // รับเป็น List ของ Int ตาม JSON
        public List<int> RoleIds { get; set; } = new List<int>();
    }
}
