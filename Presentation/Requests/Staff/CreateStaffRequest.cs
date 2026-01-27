using Microsoft.AspNetCore.Http;

namespace Presentation.Requests.Staff
{
    public class CreateStaffRequest
    {
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public IFormFile? AvatarFile { get; set; }

        // รับเป็น List ของ Int ตาม JSON
        public List<int> StaffRoles { get; set; } = new List<int>();
    }
}
