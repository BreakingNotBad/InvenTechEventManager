namespace Entities.Common
{
    // คลาสแม่แบบ (Abstract) ไม่ต้องเอาไปสร้าง Table จริง
    public abstract class BaseEntity : AuditableEntity
    {
        public bool IsDeleted { get; set; } = false;
    }
}
