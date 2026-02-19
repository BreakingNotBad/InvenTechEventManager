namespace Service.Contracts.DTOs.Staff
{
    public class UpdateStaffDto
    {
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Avatar { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? DeleteAvatar { get; set; }
        public bool? ResendInvite { get; set; }

        // ถ้าไม่ส่งมา = ไม่แก้ role
        public List<int> StaffRoles { get; set; } = [];
    }
}
