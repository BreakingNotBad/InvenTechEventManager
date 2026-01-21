namespace Service.Contracts.DTOs.Outsource
{
    public class UpdateOutsourceDto
    {
        public required string FullName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
