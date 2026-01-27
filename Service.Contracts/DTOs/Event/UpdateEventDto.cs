using Entities.Models;
using Service.Contracts.DTOs.Event;

public class UpdateEventDto
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

    public int CompanyId { get; set; }
    public int PackageId { get; set; }

    // แทนความสัมพันธ์ด้วย Id
    public List<int>? StaffIds { get; set; }
    public List<UpdateEventExtraEquipmentDto>? ExtraEquipments { get; set; }
}
