namespace Entity.Domain.Model
{
    public class Staff
    {
        public int StaffId { get; set; }
        public int EventId { get; set; }
        public int UserId { get; set; }
        public required string Fullname { get; set; }
        public string Email { get; set; } = string.Empty;
        public int Phone { get; set; }
        public required string[] Roles { get; set; }
        public DateTime AssignedAt { get; set; }
    }
}
