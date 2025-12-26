namespace Entity.Domain.Model
{
    public class EventOutsources
    {
        public int EventId { get; set; }
        public int OutsourcesId { get; set; }
        public DateTime AssignedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string Roles { get; set; } = string.Empty;
        public ICollection<Outsources> Outsources { get; set; } = new List<Outsources>();
        public ICollection<Event> Events { get; set; } = new List<Event>();
    }
}
