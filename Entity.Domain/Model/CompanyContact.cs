namespace Entity.Domain.Model
{
    public class CompanyContact
    {
        public int CompanyContactId { get; set; }
        public int CompanyId { get; set; }
        public string ContactName { get; set; } = string.Empty;
        public string? ContactEmail { get; set; }
        public string? ContactPhone { get; set; }
        public string? Role { get; set; }
        public bool IsPrimary { get; set; }
    }
}
