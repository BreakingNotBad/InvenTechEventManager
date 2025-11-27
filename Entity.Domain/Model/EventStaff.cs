namespace Entity.Domain.Model
{
    public class EventStaff
    {
        public int EventStaffId { get; set; }
        public int EventId { get; set; }
        public int UserId { get; set; }
        public DateTime AssignedAt { get; set; }
    }
}
