using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts.Manager;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/roles")]
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
