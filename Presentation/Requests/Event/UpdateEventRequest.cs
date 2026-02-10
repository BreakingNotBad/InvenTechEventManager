using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Requests.Event
{
    public class UpdateEventRequest
    {
        public string? EventName { get; set; }
        public int? EventType { get; set; } // Map จาก Enum ในรูปแบบ int
        public DateOnly? MeetingDate { get; set; }
        public TimeOnly? RegistrationTime { get; set; }
        public TimeOnly? StartTime { get; set; }
        public TimeOnly? EndTime { get; set; }
        public int? Period { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public string? Note { get; set; }
        public int? CompanyId { get; set; }
        public int? PackageId { get; set; }

        public List<IFormFile>? NewAttachmentFiles { get; set; }
        public List<int>? DeleteAttachmentIds { get; set; }
        public List<UpdateEventStaffRequest> EventStaff { get; set; } = [];
        public List<UpdateEventExtraEquipmentRequest>? EventExtraEquipments { get; set; } = [];
        public List<UpdateEventOutsourceRequest>? EventOutsources { get; set; } = [];
        public bool ForceAssign { get; set; } = false;
    }
}
