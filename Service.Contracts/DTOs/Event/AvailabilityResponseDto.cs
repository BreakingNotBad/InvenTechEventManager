using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts.DTOs.Event
{
    public class AvailabilityResponseDto
    {
        public bool IsAvailable { get; set; }
        public string? Message { get; set; }
    }
}
