using System.ComponentModel.DataAnnotations;

namespace Entity.Domain.Model
{
    public class Outsources
    {
        [Key]
        public int OutsourceId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public ICollection<EventOutsources> EventOutsources { get; set; } = new List<EventOutsources>();
    }
}
