using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts.DTOs.Event
{
    public class UpdateEventRoleRequirementDto
    {
        public int RoleId { get; set; }
        public int Quantity { get; set; }
        public WorkerSourceType SourceType { get; set; }
    }
}
