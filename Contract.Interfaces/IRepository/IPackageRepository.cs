using Contract.Interfaces.IRepository.BaseManager;
using Entity.Domain.Model;

namespace Contract.Interfaces.IRepository
{
    public interface IPackageRepository : IRepositoryBase<Package>
    {
        Task<IEnumerable<Package>> GetPackagesAsync();
        Task<Package?> GetPackageByIdAsync(int id);
        void CreatePackage(Package package);
        void UpdatePackage(Package package);
        void DeletePackage(Package package);
    }

}
