namespace Entity.Domain.Model
{
    public class Staff
    {
        public int StaffId { get; set; }
        public int EventId { get; set; }
        public int UserId { get; set; }
        public DateTime AssignedAt { get; set; }
    }
}
