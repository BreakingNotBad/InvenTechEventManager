using Contracts.DTOs;
using Entities.Models;

namespace Service.Contracts
{
    public interface IPackageService
    {
        Task<IEnumerable<Package>> GetPackagesAsync();
        Task<Package?> GetPackageByIdAsync(int id);
        Task <Package>CreatePackageAsync(CreatePackageDto dto);
        Task <Package>UpdatePackageAsync(int id, UpdatePackageDto dto);
        Task DeletePackage(int id);
    }
}
