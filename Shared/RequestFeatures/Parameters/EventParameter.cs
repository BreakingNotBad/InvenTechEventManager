using Shared.RequestFeatures.Enums;
namespace Shared.RequestFeatures.Parameters
{
    public class EventParameter
    {
        public string? EventName { get; set; }
        public EventType? EventType { get; set; }
        public TimePeriod? Period { get; set; }
        public string? Status { get; set; } 
        public string? CompanyName { get; set; }
        public int? CompanyId { get; set; }
        public string? FullName { get; set; }
    }
}
