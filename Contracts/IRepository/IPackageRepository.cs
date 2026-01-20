using Contracts.IRepository.BaseManager;
using Entities.Models;

namespace Contracts.IRepository
{
    public interface IPackageRepository : IRepositoryBase<Package>
    {
        Task<IEnumerable<Package>> GetPackagesAsync();
        Task<Package?> GetPackageByIdAsync(int id,bool trackchange);
        void CreatePackage(Package package);
        void UpdatePackage(Package package);
        void DeletePackage(Package package);
    }
}
