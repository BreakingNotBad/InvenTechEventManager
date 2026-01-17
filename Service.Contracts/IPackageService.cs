using Entities.Models;

namespace Service.Contracts
{
    public interface IPackageService
    {
        Task<IEnumerable<Package>> GetPackagesAsync();
        Task<Package?> GetPackageByIdAsync(int id);
        Task CreatePackageAsync(Package package);
        Task UpdatePackageAsync(int id, Package package);
        Task DeletePackage(int id);
    }
}
