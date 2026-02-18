using Service.Contracts.DTOs.Role;

namespace Service.Contracts.DTOs.Staff
{
    public class StaffDto
    {
        public int StaffId { get; set; }
        public string FullName { get; set; } = null!;
        public required string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Avatar { get; set; }
        public string? Status { get; set; } // "Available", "WorkingToday", "Unavailable"
        public bool IsDeleted { get; set; }
        public bool IsPending { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public ICollection<RoleDto>? StaffRoles { get; set; }
    }
}
