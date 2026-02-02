using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts.DTOs.Event
{
    public class EventAttachmentDto
    {
        public string OriginalFileName { get; set; } = null!;
        public string FilePath { get; set; } = null!;
        public string? ContentType { get; set; }
        public long FileSize { get; set; }
    }
}
