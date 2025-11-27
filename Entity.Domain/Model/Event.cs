namespace Entity.Domain.Model
{
    public class Event
    {
        public int EventId { get; set; }
        public string EventName { get; set; } = string.Empty;
        public string EventType { get; set; } = string.Empty;
        public int? QueueNumber { get; set; }
        public DateTime MeetingDate { get; set; }
        public DateTime? RehearsalInstallationDate { get; set; }
        public int CompanyId { get; set; }
        public string? Location { get; set; }
        public TimeSpan? StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }
        public TimeSpan? OfficeAppointmentTime { get; set; }
        public TimeSpan? VenueAppointmentTime { get; set; }
        public TimeSpan? RegistrationOpenTime { get; set; }
        public string? QuoteNumber { get; set; }
        public DateTime? QuoteIssueDate { get; set; }
        public int? QuoteStatusId { get; set; }
        public int PackageId { get; set; }
        public string? Notes { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
