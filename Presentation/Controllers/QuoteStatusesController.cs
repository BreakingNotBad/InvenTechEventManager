using Entity.Domain.Model;
using Microsoft.AspNetCore.Mvc;
using Service.Contract;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuoteStatusesController : ControllerBase
    {
        private readonly IServiceManager _service;

        public QuoteStatusesController(IServiceManager service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetQuoteStatuses()
        {
            var items = await _service.QuoteStatus.GetQuoteStatusesAsync();
            return Ok(items);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetQuoteStatusById(int id)
        {
            var item = await _service.QuoteStatus.GetQuoteStatusByIdAsync(id);
            if (item == null) return NotFound();

            return Ok(item);
        }
    }
}
