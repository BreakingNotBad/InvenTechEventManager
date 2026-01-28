using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts.DTOs.Package;
using Service.Contracts.Manager;
using Shared.RequestFeatures.Parameters;

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
        public async Task<IActionResult> GetPackages(
            [FromQuery] PackageParameter packageParameter)
        {
            var items = await _service.Package.GetPackagesAsync(packageParameter);
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
        public async Task<IActionResult> CreatePackage(CreatePackageDto dto)
        {
            var createdPackage = await _service.Package.CreatePackageAsync(dto);
            return CreatedAtAction(
                nameof(GetPackageById), 
                new { id = createdPackage.PackageId }, 
                createdPackage);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdatePackage(int id, UpdatePackageDto dto)
        {
            await _service.Package.UpdatePackageAsync(id, dto);
            return Ok(dto);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeletePackage(int id)
        {
            await _service.Package.DeletePackage(id);
            return NoContent();
        }
    }
}
