using Microsoft.AspNetCore.Mvc;
using Service.Contracts.Manager;
using Shared.RequestFeatures.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public async Task<IActionResult> GetRole([FromQuery]RoleParameter roleParameter)
        {
            var roleList = await _service.Role.GetRoleByAsync(roleParameter);
            return Ok(roleList);
        }
    }
}
