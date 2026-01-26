using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts.DTOs.Event
{
    public class CreateEventDto
    {
        // Basic info
        public string EventName { get; set; } = null!;
        public EventType EventType { get; set; }

        // Date & Time
        public DateOnly MeetingDate { get; set; }
        public TimeOnly RegistrationTime { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public TimePeriod Period { get; set; }

        // Location
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public string? Note { get; set; }

        // Relations (by Id only)
        public int CompanyId { get; set; }
        public int PackageId { get; set; }

        // Staff ที่เข้าร่วม (optional)
        public List<int>? StaffIds { get; set; }

        // Extra equipment (optional)
        public List<CreateEventExtraEquipmentDto>? ExtraEquipments { get; set; }
    }
}
