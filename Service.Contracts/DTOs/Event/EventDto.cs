using Entities.Models;
using Service.Contracts.DTOs.Company;
using Service.Contracts.DTOs.Event;
using Service.Contracts.DTOs.Package;
using Service.Contracts.DTOs.Staff;
public class EventDto
{
    public int EventId { get; set; }
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

    // Related (read-friendly)
    public CompanyDto Company { get; set; } = null!;
    public PackageDto Package { get; set; } = null!;
    public StaffDto CreatedBy { get; set; } = null!;

    public List<StaffDto> Staffs { get; set; } = [];
    public List<EventExtraEquipmentDto> ExtraEquipments { get; set; } = [];
    public List<EventAttachmentDto> Attachments { get; set; } = [];
}
