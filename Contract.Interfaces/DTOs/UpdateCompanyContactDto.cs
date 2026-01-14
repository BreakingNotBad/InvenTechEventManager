namespace Contract.DTOs.Company
{
    public class UpdateCompanyContactDto
    {
        public int? CompanyContactId { get; set; } // null = new
        public string FullName { get; set; } = null!;
        public string? Email { get; set; }
        public bool IsPrimary { get; set; } = false;
        public bool IsDeleted { get; set; } = false;
    }
}
