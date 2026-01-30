using Entities.Models;
using Presentation.Requests.Event;
using Service.Contracts.DTOs.CompanyContact;

namespace Service.Contracts.DTOs.Event
{
    public class CreateEventDto
    {
        public string EventName { get; set; } = null!;
        public EventType EventType { get; set; }
        public DateOnly MeetingDate { get; set; }
        public TimeOnly RegistrationTime { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public TimePeriod Period { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public string? Note { get; set; }
        public int CreatedByStaffId { get; set; }
        public int CompanyId { get; set; }
        public int PackageId { get; set; }

        // ไฟล์แนบที่ Save ลง Disk แล้ว (เก็บเป็น Path)
        public List<EventAttachmentDto> Attachments { get; set; } = [];
        public List<int> StaffIds { get; set; } = [];
        public List<CreateEventExtraEquipmentDto> EventExtraEquipments { get; set; } = [];
        public List<CreateEventOutsourceDto> EventOutsources { get; set; } = [];
    }

    public class EventAttachmentDto
    {
        public string OriginalFileName { get; set; } = null!;
        public string FilePath { get; set; } = null!;
        public string? ContentType { get; set; }
        public long FileSize { get; set; }
    }
}