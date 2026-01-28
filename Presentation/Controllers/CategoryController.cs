using Microsoft.AspNetCore.Mvc;
using Service.Contracts.Manager;
using Shared.RequestFeatures.Parameters;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/categories")]
    public class CategoryController : ControllerBase
    {
        private readonly IServiceManager _service;

        public CategoryController(IServiceManager service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategory([FromQuery] CategoryParameter categoryParameter)
        {
            var categoryList = await _service.Category.GetCategoryByAsync(categoryParameter);
            return Ok(categoryList);
        }
    }
}
