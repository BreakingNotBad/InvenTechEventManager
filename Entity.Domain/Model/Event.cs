using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Domain.Model
{
    public enum TimePeriod
    {
        Morning,
        Afternoon,
    }

    public class Event
    {
        [Key]
        public int EventId { get; set; }

        [MaxLength(255)]
        public required string EventName { get; set; }

        [MaxLength(50)]
        public required string EventType { get; set; }

        public required DateOnly MeetingDate { get; set; }
        public required TimeOnly RegistrationTime { get; set; }
        public required TimeOnly StartTime { get; set; }
        public required TimeOnly EndTime { get; set; }

        public required TimePeriod Period { get; set; }

        [Column(TypeName = "decimal(18, 15)")]
        public decimal Latitude { get; set; }

        [Column(TypeName = "decimal(18, 15)")]
        public decimal Longitude { get; set; }

        [MaxLength(2000)]
        public string? Note { get; set; }

        // 5. Best Practice: เก็บเป็น URL/Path แทนไฟล์ Binary
        // ถ้ามีหลายไฟล์ แนะนำให้แยกเป็น Table "EventAttachments" ต่างหาก
        [MaxLength(500)]
        public string? DocumentUrl { get; set; }

        public bool IsDeleted { get; set; } = false;

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        // --- Relations ---

        // Staff
        public int CreatedByStaffId { get; set; }

        [ForeignKey(nameof(CreatedByStaffId))]
        public Staff CreatedByStaff { get; set; } = null!;

        // Company
        public int CompanyId { get; set; }

        [ForeignKey(nameof(CompanyId))]
        public Company Company { get; set; } = null!;

        // Package
        public int PackageId { get; set; }

        [ForeignKey(nameof(PackageId))]
        public Package Package { get; set; } = null!;

        // Collections
        public ICollection<EventStaff> EventStaff { get; set; } = new List<EventStaff>();
        public ICollection<EventOutsource> EventOutsources { get; set; } = new List<EventOutsource>();
        public ICollection<EventExtraEquipment> EventExtraEquipments { get; set; } = new List<EventExtraEquipment>();
    }
}