using Microsoft.AspNetCore.Mvc;
using Service.Contract;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompanyContactController : ControllerBase
    {
        private readonly IServiceManager _service;
        public CompanyContactController(IServiceManager service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetCompanyContactsAsync()
        {
            var contactsList = await _service.CompanyContact.GetCompanyContactsAsync();
            return Ok(contactsList);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCompanyContactByIdAsync(int id)
        {
            var contact = await _service.CompanyContact.GetCompanyContactByIdAsync(id);
            if (contact == null) return NotFound();
            return Ok(contact);
        }
    }
}
