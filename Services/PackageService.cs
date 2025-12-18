using Contract.Interfaces.IRepository;
using Entity.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Service.Contract;

namespace Service
{
    public class PackageService : IPackageService
    {
        private readonly IRepositoryManager _repo;

        public PackageService(IRepositoryManager repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<Package>> GetPackagesAsync()
        {
            return await _repo.Package.GetPackagesAsync();
        }

        public async Task<Package?> GetPackageByIdAsync(int id)
        {
            return await _repo.Package.GetPackageByIdAsync(id);
        }
    }
}
