namespace Entity.Domain.Model
{
    public class Companies
    {
        public int CompanyId { get; set; }
        public string CompanyName { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public ICollection<CompanyContacts> CompanyContacts { get; set; } = new List<CompanyContacts>();
        public ICollection<Event> Events { get; set; } = new List<Event>();
    }
}
