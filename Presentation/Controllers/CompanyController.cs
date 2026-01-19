using Contracts.DTOs;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts.Manager;

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
            string? companyName, 
            string? companyContact, 
            string? Address,
            decimal? Latitude,
            decimal? Longitude,
            bool IsDeleted,
            DateTime CreatedAt,
            DateTime UpdatedAt)
        {
            Console.WriteLine("Hello woRld");
            var companiesList = await _service.Company.GetCompaniesAsync(
                companyName,
                companyContact,
                Address,
                Latitude,
                Longitude,
                IsDeleted,
                CreatedAt,
                UpdatedAt
            );

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

        [HttpPost]
        public async Task<IActionResult> CreateCompany(CreateCompanyDto dto)
        {
            var createCompany = await _service.Company.CreateCompanyAsync(dto);

            return CreatedAtAction(
                nameof(GetCompanyById),
                new { id = createCompany.CompanyId },
                createCompany
            );
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateCompany(int id, [FromBody] UpdateCompanyDto dto)
        {
            await _service.Company.UpdateCompanyAsync(id, dto);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteCompany(int id)
        {
            await _service.Company.DeleteCompanyAsync(id);
            return NoContent();
        }
    }
}
