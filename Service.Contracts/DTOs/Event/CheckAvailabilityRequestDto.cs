using Entities.Models;
using Shared.RequestFeatures.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts.DTOs.Event
{
    public class CheckAvailabilityRequestDto
    {
        public int? EventId { get; set; }     // สำหรับตอน Update
        public int? StaffId { get; set; }
        public int? OutsourceId { get; set; }
        public DateOnly MeetingDate { get; set; }
        public TimePeriod Period { get; set; }
    }
}
