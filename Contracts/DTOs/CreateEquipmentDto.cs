using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.DTOs
{
    public class CreateEquipmentDto
    {
        public required string EquipmentName { get; set; }
        public int CategoryId { get; set; }
    }
}
