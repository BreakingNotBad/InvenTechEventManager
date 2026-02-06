using System.ComponentModel.DataAnnotations.Schema;
using Entities.Common;

namespace Entities.Models
{
    public enum TimePeriod
    {
        Morning=1,
        Afternoon=2,
    }

    public enum EventType
    {
        Offline=1,
        Hybrid=2,
        Online=3,
    }

    public class Event : BaseEntity
    {
        public int EventId { get; set; }
        public required string EventName { get; set; }
        public required EventType EventType { get; set; }
        public required DateOnly MeetingDate { get; set; }
        public required TimeOnly RegistrationTime { get; set; }
        public required TimeOnly StartTime { get; set; }
        public required TimeOnly EndTime { get; set; }
        public required TimePeriod Period { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public string? Note { get; set; }

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
        public int? PackageId { get; set; }

        [ForeignKey(nameof(PackageId))]
        public Package? Package { get; set; }

        // Navigation Properties
        public ICollection<EventAttachment> EventAttachments { get; set; } = [];
        public ICollection<EventStaff> EventStaff { get; set; } = [];
        public ICollection<EventOutsource> EventOutsources { get; set; } = [];
        public ICollection<EventExtraEquipment> EventExtraEquipments { get; set; } = [];
    }
}
