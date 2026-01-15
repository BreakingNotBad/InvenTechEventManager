using Microsoft.AspNetCore.Http;

namespace Presentation.Requests.StaffRequests
{
    public class CreateStaffRequest
    {
        public required string FullName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public IFormFile? AvatarFile { get; set; }

        // รับเป็น List ของ Int ตาม JSON
        public List<int> RoleIds { get; set; } = new List<int>();
    }
}
