namespace Entities.Models
{
    public class Outsource
    {
        public int OutsourceId { get; set; }
        public required string FullName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        // Navigation Property
        public ICollection<EventOutsource>? EventOutsources { get; set; } = [];
    }
}
