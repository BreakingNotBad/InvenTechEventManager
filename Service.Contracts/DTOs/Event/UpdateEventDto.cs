using Entities.Models;
using Service.Contracts.DTOs.Event;

public class UpdateEventDto
{
    public string? EventName { get; set; }
    public EventType? EventType { get; set; }
    public DateOnly? MeetingDate { get; set; }
    public TimeOnly? RegistrationTime { get; set; }
    public TimeOnly? StartTime { get; set; }
    public TimeOnly? EndTime { get; set; }
    public TimePeriod? Period { get; set; }

    public decimal? Latitude { get; set; }
    public decimal? Longitude { get; set; }
    public string? Note { get; set; }

    public int? CompanyId { get; set; }
    public int? PackageId { get; set; }

    // แทนความสัมพันธ์ด้วย Id
    public List<UpdateEventStaffDto> EventStaffs { get; set; } = [];
    public List<UpdateEventExtraEquipmentDto>? EventExtraEquipments { get; set; }
    public List<UpdateEventOutsourceDto>? EventOutsources { get; set; }
    public List<EventAttachmentDto>? NewAttachments { get; set; }
}
