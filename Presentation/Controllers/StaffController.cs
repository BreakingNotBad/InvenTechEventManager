using Microsoft.AspNetCore.Mvc;
using Service.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    public class StaffController : ControllerBase
    {
        private readonly IServiceManager _service;
        public StaffController(IServiceManager service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetStaff()
        {
            var staffList = await _service.Staff.GetStaffMembersAsync();
            return Ok(staffList);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetStaffById(int id)
        {
            var company = await _service.Company.GetCompanyByIdAsync(id);
            if (company == null) return NotFound();
            return Ok(company);
        }
    }
}
