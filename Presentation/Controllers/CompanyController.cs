using Entity.Domain.Model;
using Microsoft.AspNetCore.Mvc;
using Service.Contract.Manager;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/companies")]
    public class CompanyController : ControllerBase
    {
        private readonly IServiceManager _service;

        public CompanyController(IServiceManager service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetCompanies(
            [FromQuery(Name = "q")] string? query)
        {
            var companiesList = await _service.Company.GetCompaniesAsync(query);
            return Ok(companiesList);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCompanyById(int id)
        {
            var company = await _service.Company.GetCompanyByIdAsync(id);
            if (company == null)
                return NotFound();
            return Ok(company);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteCompany(int id)
        {
            await _service.Company.DeleteCompanyAsync(id);
            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCompany([FromBody] Company company)
        {
            if (company == null)
                return BadRequest();

            await _service.Company.CreateCompanyAsync(company);
            return CreatedAtAction(nameof(GetCompanyById), new { id = company.CompanyId }, company);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateCompany(
            int id,
            [FromBody] Company company
        )
        {
            await _service.Company.UpdateCompanyAsync(id, company);
            return NoContent();
        }

        //GetCompanyContacts
        [HttpGet("cc/{id:int}")]
        public async Task<IActionResult> GetCompanyContactByCompanyId(int id)
        {
            var companycontact = await _service.Company.GetCompanyContactByCompanyIdAsync(id);
            return Ok(companycontact);
        }
    }
}
