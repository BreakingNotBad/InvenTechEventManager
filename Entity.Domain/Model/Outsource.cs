using System.ComponentModel.DataAnnotations;

namespace Entity.Domain.Model
{
    public class Outsource
    {
        [Key]
        public int OutsourceId { get; set; }
        public required string FullName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public ICollection<EventOutsource> EventOutsources { get; set; } = new List<EventOutsource>();
    }
}
