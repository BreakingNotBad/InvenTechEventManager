using Entity.Domain.Model;

namespace Service.Contract
{
    public interface IPackageService
    {
        Task<IEnumerable<Packages>> GetPackagesAsync();
        Task<Packages?> GetPackageByIdAsync(int id);
        Task CreatePackageAsync(Packages package);
        Task DeletePackage(int id);
    }
}
