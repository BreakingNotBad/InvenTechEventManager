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
    public class CategoryController:ControllerBase
    {
        private readonly IServiceManager _service;

        public CategoryController(IServiceManager service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> GetCategory()
        {
            var categoryList = await _service.Category.GetCategoryByAsync();
            return Ok(categoryList);
        }
    }
}
