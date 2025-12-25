namespace Entity.Domain.Model
{
    public class Staff
    {
        public int StaffId { get; set; }
        public required string FullName { get; set; }
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public required string[] Roles { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
