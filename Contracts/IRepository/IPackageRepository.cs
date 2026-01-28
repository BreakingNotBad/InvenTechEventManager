using Contracts.IRepository.BaseManager;
using Entities.Models;
using Shared.RequestFeatures.Parameters;

namespace Contracts.IRepository
{
    public interface IPackageRepository : IRepositoryBase<Package>
    {
        Task<IEnumerable<Package>> GetPackagesAsync(PackageParameter packageParameter, bool trackChanges);
        Task<Package?> GetPackageByIdAsync(int id,bool trackchange);
        void CreatePackage(Package package);
        void UpdatePackage(Package package);
        void DeletePackage(Package package);
    }
}
