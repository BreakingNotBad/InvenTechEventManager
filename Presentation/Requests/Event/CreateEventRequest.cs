using Microsoft.AspNetCore.Http;
using Service.Contracts.DTOs.Event;

namespace Presentation.Requests.Event
{
    public class CreateEventRequest
    {
        public string? EventName { get; set; }
        public int? EventType { get; set; } // Map จาก Enum ในรูปแบบ int
        public DateOnly? MeetingDate { get; set; }
        public TimeOnly? RegistrationTime { get; set; }
        public TimeOnly? StartTime { get; set; }
        public TimeOnly? EndTime { get; set; }
        public int? Period { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public string? Note { get; set; }
        public int? CreatedByStaffId { get; set; }
        public int? CompanyId { get; set; }
        public int? PackageId { get; set; }


        public List<IFormFile>? AttachmentFiles { get; set; }
        public List<CreateEventStaffRequest> EventStaff { get; set; } = [];
        public List<CreateEventExtraEquipmentRequest>? EventExtraEquipments { get; set; } = [];
        public List<CreateEventOutsourceRequest>? EventOutsources { get; set; } = [];
    }
}