using Entities.Models;
using Service.Contracts.DTOs.Company;
using Service.Contracts.DTOs.Outsource;
using Service.Contracts.DTOs.Package;
using Service.Contracts.DTOs.Staff;

namespace Service.Contracts.DTOs.Event
{
    public class EventDto
    {
        public int EventId { get; set; }
        public string EventStatus { get; set; } = null!;
        public string EventName { get; set; } = null!;
        public string EventType { get; set; } = null!;
        public string Period { get; set; } = null!;
        public DateOnly MeetingDate { get; set; }
        public TimeOnly RegistrationTime { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public string? Address { get; set; }
        public string? Note { get; set; }


        public CompanyDto Company { get; set; } = null!;
        public PackageDto Package { get; set; } = null!;
        public StaffDto CreatedByStaff { get; set; } = null!;


        public ICollection<EventAttachmentDto> EventAttachments { get; set; } = [];
        public ICollection<EventRoleRequirementDto> Requirements { get; set; } = [];
        public ICollection<EventStaffDto> EventStaff { get; set; } = [];
        public ICollection<EventOutsourceDto> EventOutsources { get; set; } = [];
        public ICollection<EventExtraEquipmentDto> EventExtraEquipments { get; set; } = [];
    }
}