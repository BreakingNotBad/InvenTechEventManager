using Microsoft.AspNetCore.Mvc;
using Service.Contract;


namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompanyController: ControllerBase
    {
        private readonly IServiceManager _service;
        public CompanyController(IServiceManager service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetCompanies()
        {
            var companiesList = await _service.Company.GetCompaniesAsync();
            return Ok(companiesList);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCompanyById(int id)
        {
            var company = await _service.Company.GetCompanyByIdAsync(id);
            if (company == null) return NotFound();
            return Ok(company);
        }
    }
}
