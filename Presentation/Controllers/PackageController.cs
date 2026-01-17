using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts.Manager;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/packages")]
    public class PackageController : ControllerBase
    {
        private readonly IServiceManager _service;

        public PackageController(IServiceManager service)
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
            if (item == null)
                return NotFound();

            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePackage([FromBody] Package package)
        {
            await _service.Package.CreatePackageAsync(package);
            return CreatedAtAction(nameof(GetPackageById), new { id = package.PackageId }, package);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdatePackage(int id, [FromBody] Package package)
        {
            await _service.Package.UpdatePackageAsync(id, package);
            return Ok(package);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeletePackage(int id)
        {
            await _service.Package.DeletePackage(id);
            return NoContent();
        }
    }
}
