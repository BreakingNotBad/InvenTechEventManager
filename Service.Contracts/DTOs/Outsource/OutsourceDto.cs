namespace Service.Contracts.DTOs.Outsource
{
    public class OutsourceDto
    {
        public int OutsourceId { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public bool IsDeleted { get; set; }
    }
}
