using Entity.Domain.Model;

namespace Service.Contract
{
    public interface IPackageService
    {
        Task<IEnumerable<Package>> GetPackagesAsync();
        Task<Package?> GetPackageByIdAsync(int id);
    }
}
