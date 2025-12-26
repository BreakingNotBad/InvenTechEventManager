using Contract.Interfaces.IRepository;
using Entity.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Repository.Infrastructure.Data;
using Repository.Repositories;

namespace Repository.Infrastructure.Repository
{
    public class PackageRepository : RepositoryBase<Packages>, IPackageRepository
    {
        public PackageRepository(RepositoryContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Packages>> GetPackagesAsync()
        {
            return await FindAll(trackChanges: false).ToListAsync();
        }

        public async Task<Packages?> GetPackageByIdAsync(int id)
        {
            return await FindByCondition(e => e.PackageId == id, trackChanges: false)
                .FirstOrDefaultAsync();
        }
        public void CreatePackage(Packages package)
        {
            Create(package);
        }
        public async void DeletePackage(Packages package)
        {
            Delete(package);
            await Task.CompletedTask;
        }
    }
}
