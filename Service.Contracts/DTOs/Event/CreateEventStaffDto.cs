using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts.DTOs.Event
{
    public class CreateEventStaffDto
    {
        public int StaffId { get; set; }
        public int RoleId { get; set; }
    }
}
