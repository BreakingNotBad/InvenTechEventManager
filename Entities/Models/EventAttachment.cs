using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class EventAttachment
    {

        public int EventAttachmentId { get; set; }

        // ชื่อไฟล์ต้นฉบับที่ user อัปโหลด (เช่น "Project_Brief.pdf")
        // เอาไว้แสดงให้ User เห็นชื่อสวยๆ

        public required string OriginalFileName { get; set; }

        // Path ที่เก็บไฟล์จริงใน Server/Cloud (เช่น "uploads/events/guid_123.pdf")

        public required string FilePath { get; set; }

        // ประเภทไฟล์ (Optional) เช่น "application/pdf", "image/png"

        public string? ContentType { get; set; }

        // ขนาดไฟล์ (Optional) เก็บเป็น bytes เผื่อเอาไปคำนวณพื้นที่
        public long FileSize { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Relation กลับไปหา Event
        public int EventId { get; set; }

        [ForeignKey(nameof(EventId))]
        public Event Event { get; set; } = null!;
    }
}
