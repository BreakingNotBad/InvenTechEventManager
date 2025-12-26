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

        [HttpGet("GetCompanyBy{id:int}")]
        public async Task<IActionResult> GetCompanyById(int id)
        {
            var company = await _service.Company.GetCompanyByIdAsync(id);
            if (company == null) return NotFound();
            return Ok(company);
        }

        [HttpGet("Dropdown")]
        public async Task<IActionResult> GetCompanyDropdown([FromQuery] string? query)
        {
            var companies = (await _service.Company.GetCompaniesAsync()).AsQueryable();

            // filter ตาม query (ค้นชื่อบริษัท)
            if (!string.IsNullOrWhiteSpace(query))
            {
                var q = query.Trim().ToLower();
                companies = companies.Where(c => (c.CompanyName ?? "").ToLower().Contains(q));
            }

            // ส่งออกเฉพาะ id + name
            var items = companies
                .OrderBy(c => c.CompanyName)
                .Select(c => new
                {
                    company_id = c.CompanyId,
                    company_name = c.CompanyName
                })
                .ToList();

            return Ok(items);
        }

        [HttpDelete("DeleteCompanyBy{id:int}")]
        public async Task<IActionResult> DeleteCompany(int id)
        {
            await _service.Company.DeleteCompanyAsync(id);
            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCompany([FromBody] Entity.Domain.Model.Company company)
        {
            if (company == null) return BadRequest();

            //ห้าม client กำหนด CompanyId ตอนสร้าง
            company.CompanyId = 0;

            await _service.Company.CreateCompanyAsync(company);

            return CreatedAtAction(
                nameof(GetCompanyById),
                new { id = company.CompanyId },
                company
            );
        }

        [HttpPut("UpdateCompanyBy{id:int}")]
        public async Task<IActionResult> UpdateCompany(int id, [FromBody] Entity.Domain.Model.Company company)
        {
            if (id != company.CompanyId)
            {
                return BadRequest("ID in URL does not match ID in body.");
            }
            await _service.Company.UpdateCompanyAsync(company);
            return NoContent();
        }


        //GetCompanyContacts
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCompanyContactByCompanyId(int id)
        {
            var companycontact = await _service.Company.GetCompanyContactByCompanyIdAsync(id);
            return Ok(companycontact);
        }
    }
}
