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

        public async Task<Package?> GetPackageByIdAsync(int id,bool trackchange)
        {
            return await FindByCondition(e => e.PackageId == id, trackchange)
                .Include(e => e.EquipmentSets)
                .ThenInclude(eq => eq.Equipment)
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
