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
        public async Task CreatePackageAsync(Package package)
        {
            _repo.Package.CreatePackage(package);
            await _repo.SaveAsync();
        }
        public async Task DeletePackage(int id)
        {
            var existingPackage = await _repo.Package.GetPackageByIdAsync(id);
            if (existingPackage == null)
            {
                throw new ArgumentException($"Package with id {id} not found.");
            }
            _repo.Package.DeletePackage(existingPackage);
            await _repo.SaveAsync();
        }
    }
}
