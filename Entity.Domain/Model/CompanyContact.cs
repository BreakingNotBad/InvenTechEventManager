using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Domain.Model
{
    public class CompanyContact
    {
        [Key]
        public int CompanyContactId { get; set; }

        [MaxLength(255)]
        public required string FullName { get; set; }

        [MaxLength(255)]
        [EmailAddress]
        public string? Email { get; set; }

        [MaxLength(50)]
        [Phone]
        public string? PhoneNumber { get; set; }

        [MaxLength(100)]
        public string? Position { get; set; }

        public bool IsPrimary { get; set; } = false;

        public bool IsDeleted { get; set; } = false;

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        // Foreign Key
        public int CompanyId { get; set; }

        [ForeignKey(nameof(CompanyId))]
        public Company? Company { get; set; } // ใส่ ? ไว้เผื่อกรณีที่เรา New Object แต่ยังไม่ได้ Load Company มา
    }
}