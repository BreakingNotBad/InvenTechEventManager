using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts.DTOs.Event
{
    public class EventAttachmentDto
    {
        public int EventAttachmentId { get; set; }
        public string OriginalFileName { get; set; } = null!;
        public string ContentType { get; set; } = null!;
        public long FileSize { get; set; }
        public DateTime CreatedAt { get; set; }

        // สำหรับ frontend ใช้โหลดไฟล์
        public string DownloadUrl { get; set; } = null!;
    }
}
