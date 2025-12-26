using Entity.Domain.Model;
using Microsoft.AspNetCore.Mvc;
using Service.Contract;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PackagesController : ControllerBase
    {
        private readonly IServiceManager _service;

        public PackagesController(IServiceManager service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetPackages()
        {
            var items = await _service.Package.GetPackagesAsync();
            return Ok(items);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetPackageById(int id)
        {
            var item = await _service.Package.GetPackageByIdAsync(id);
            if (item == null) return NotFound();

            return Ok(item);
        }
        [HttpPost]
        public async Task<IActionResult> CreatePackage([FromBody] Packages package)
        {
            await _service.Package.CreatePackageAsync(package);
            return CreatedAtAction(nameof(GetPackageById), new { id = package.PackageId }, package);
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeletePackage(int id)
        {
            await _service.Package.DeletePackage(id);
            return NoContent();
        }
    }
}
