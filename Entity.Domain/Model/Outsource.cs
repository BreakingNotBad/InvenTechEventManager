namespace Entity.Domain.Model
{
    public class Outsource
    {
        public int OutsourceId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Notes { get; set; }
    }
}
