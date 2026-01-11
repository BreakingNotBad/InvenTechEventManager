using System.ComponentModel.DataAnnotations;

namespace Entity.Domain.Model
{
    public class Outsource
    {
        [Key]
        public int OutsourceId { get; set; }

        [MaxLength(255)]
        public required string FullName { get; set; }

        [MaxLength(255)]
        [EmailAddress]
        public string? Email { get; set; }

        [MaxLength(50)]
        [Phone]
        public string? PhoneNumber { get; set; }

        public bool IsDeleted { get; set; } = false;

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        // Navigation Properties
        public ICollection<EventOutsource> EventOutsources { get; set; } = new List<EventOutsource>();
    }
}
