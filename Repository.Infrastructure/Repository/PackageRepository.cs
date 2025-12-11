using Contract.Interfaces.IRepository;
using Entity.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Repository.Infrastructure.Data;
using Repository.Repositories;

namespace Repository.Infrastructure.Repository
{
    public class PackageRepository : RepositoryBase<Package>, IPackageRepository
    {
        public PackageRepository(RepositoryContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Package>> GetPackagesAsync()
        {
            return await FindAll(trackChanges: false).ToListAsync();
        }

        public async Task<Package?> GetPackageByIdAsync(int id)
        {
            return await FindByCondition(e => e.PackageId == id, trackChanges: false)
                .FirstOrDefaultAsync();
        }
    }
}
