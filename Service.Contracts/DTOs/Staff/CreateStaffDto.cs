namespace Service.Contracts.DTOs.Staff
{
    public class CreateStaffDto
    {
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Avatar { get; set; }

        // รับเป็น List ของ Int ตาม JSON
        public List<int> RoleIds { get; set; } = new List<int>();
    }
}
