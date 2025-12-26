using Entity.Domain.Model;

namespace Contract.Interfaces.IRepository
{
    public interface IPackageRepository : IRepositoryBase<Packages>
    {
        Task<IEnumerable<Packages>> GetPackagesAsync();
        Task<Packages?> GetPackageByIdAsync(int id);
        void CreatePackage(Packages package);
        void DeletePackage(Packages package);
    }

}
