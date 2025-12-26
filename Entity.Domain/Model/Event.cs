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
        public required string EventName { get; set; }
        public required string EventType { get; set; }
        public required DateOnly MeetingDate { get; set; }
        public required TimeOnly RegistrationTime { get; set; }
        public required TimeOnly StartTime { get; set; }
        public required TimeOnly EndTime { get; set; }
        public required TimePeriod Period { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string? Note { get; set; }
        public byte[]? Documents { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        // Relations
        [ForeignKey(nameof(CreatedByStaff))]
        public int CreatedByStaffId { get; set; }
        public Staff CreatedByStaff { get; set; } = null!;

        [ForeignKey(nameof(Company))]
        public int CompanyId { get; set; }
        public Company Company { get; set; } = null!;

        [ForeignKey(nameof(Package))]
        public int PackageId { get; set; }
        public Package Package { get; set; } = null!;

        public ICollection<EventStaff> EventStaff { get; set; } = new List<EventStaff>();
        public ICollection<EventOutsource> EventOutsources { get; set; } = new List<EventOutsource>();
        public ICollection<EventExtraEquipment> EventExtraEquipments { get; set; } = new List<EventExtraEquipment>();
    }
}
