using Entity.Domain.Model;

namespace Contract.Interfaces.IRepository
{
    public interface IPackageRepository : IRepositoryBase<Package>
    {
        Task<IEnumerable<Package>> GetPackagesAsync();
        Task<Package?> GetPackageByIdAsync(int id);
    }
}
