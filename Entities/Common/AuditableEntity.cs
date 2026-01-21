using Entities.Interfaces;

namespace Entities.Common
{
    public abstract class AuditableEntity : IAuditable
    {
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
