namespace Service.Contracts.DTOs.Outsource
{
    public class CreateOutsourceDto
    {
        public required string FullName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
