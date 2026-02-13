namespace Service.Contracts.DTOs.Outsource
{
    public class OutsourceDto
    {
        public int OutsourceId { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? Status { get; set; } // "Available", "WorkingToday", "Unavailable"
        public string? PhoneNumber { get; set; }
        public bool IsDeleted { get; set; }
    }
}
