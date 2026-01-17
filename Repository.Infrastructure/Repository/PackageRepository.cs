using Contracts.IRepository;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Data;
using Repositories.Repository.BaseManager;

namespace Repositories.Repository
{
    public class PackageRepository : RepositoryBase<Package>, IPackageRepository
    {
        public PackageRepository(RepositoryContext context)
            : base(context) { }

        public async Task<IEnumerable<Package>> GetPackagesAsync()
        {
            return await FindAll(trackChanges: false)
                .Include(e => e.EquipmentSets)
                    .ThenInclude(es => es.Equipment)
                .ToListAsync();
        }

        public async Task<Package?> GetPackageByIdAsync(int id)
        {
            return await FindByCondition(e => e.PackageId == id, trackChanges: false)
                .FirstOrDefaultAsync();
        }

        public void CreatePackage(Package package)
        {
            Create(package);
        }

        public void UpdatePackage(Package package)
        {
            Update(package);
        }

        public void DeletePackage(Package package)
        {
            Delete(package);
        }
    }
}
