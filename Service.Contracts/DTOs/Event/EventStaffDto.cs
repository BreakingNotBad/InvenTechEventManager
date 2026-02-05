using Service.Contracts.DTOs.Role;
using Service.Contracts.DTOs.Staff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts.DTOs.Event
{
    public class EventStaffDto
    {
        public StaffDto Staff { get; set; } = null!;
        public RoleDto Role { get; set; } = null!;
    }
}
