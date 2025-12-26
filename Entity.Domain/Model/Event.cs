using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;

namespace Entity.Domain.Model
{
    public class Event
    {
        [Key]
        public int EventId { get; set; }
        public string EventName { get; set; } = string.Empty;
        public string EventType { get; set; }
        public string? Location { get; set; }
        public TimeSpan? StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }
        public DateTime MeetingDate { get; set; }
        public TimeSpan? RegistrationTime { get; set; }
        public int PackageId { get; set; }
        public string? Note { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public enum TimePeriod
        {
            Morning,
            Afternoon,
        }
        public byte[] Document { get; set; } 

        public int CreatedByStaffId { get; set; }          // FK
        public Staff CreatedByStaff { get; set; } = null!; // Navigation ไป Staff
        public int CompanyId { get; set; }               // FK
        public Companies Companies { get; set; } = null!;  // Navigation ไป Company
        public ICollection<EventStaff> EventStaff { get; set; } = new List<EventStaff>();
        public ICollection<EventOutsources> EventOutsources { get; set; } = new List<EventOutsources>();
        public ICollection<EventExtraEquipments> EventExtraEquipments { get; set; } = new List<EventExtraEquipments>();
        public ICollection<Packages> Packages { get; set; } = new List<Packages>();

    }
}
