namespace Entity.Domain.Model
{
    public class Company
    {
        public int CompanyId { get; set; }
        public string? CompanyName { get; set; }
        public int? PrimaryContactId { get; set; }
    }
}
