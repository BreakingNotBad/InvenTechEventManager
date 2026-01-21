namespace Service.Contracts.DTOs.CompanyContact
{
    public class CompanyContactDto
    {
        public int CompanyContactId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Position { get; set; }
        public bool IsPrimary { get; set; }
    }
}
