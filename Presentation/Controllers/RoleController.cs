using Microsoft.AspNetCore.Mvc;
using Service.Contract.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoleController : ControllerBase
    {
        private readonly IServiceManager _service;
        public RoleController(IServiceManager service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> GetRole()
        {
            var roleList = await _service.Role.GetRoleByAsync();
            return Ok(roleList);
        }
    }
}
