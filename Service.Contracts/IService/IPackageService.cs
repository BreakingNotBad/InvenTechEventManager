using Entities.Models;
using Service.Contracts.DTOs.Package;

namespace Service.Contracts.IService
{
    public interface IPackageService
    {
        Task<IEnumerable<PackageDto>> GetPackagesAsync();
        Task<PackageDto?> GetPackageByIdAsync(int id);
        Task <PackageDto>CreatePackageAsync(CreatePackageDto dto);
        Task <PackageDto>UpdatePackageAsync(int id, UpdatePackageDto dto);
        Task DeletePackage(int id);
    }
}
