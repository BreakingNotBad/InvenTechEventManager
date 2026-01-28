using Contracts.IRepository;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Data;
using Repositories.Repository.BaseManager;
using Shared.RequestFeatures.Parameters;

namespace Repositories.Repository
{
    public class PackageRepository : RepositoryBase<Package>, IPackageRepository
    {
        public PackageRepository(RepositoryContext context)
            : base(context) { }

        public async Task<IEnumerable<Package>> GetPackagesAsync(PackageParameter packageParameter, bool trackChanges)
        {
            // 1. เริ่มต้น Query
            var items = FindAll(trackChanges);

            // 2. Filter: Search by FullName
            if (!string.IsNullOrWhiteSpace(packageParameter.PackageName))
            {
                items = items.Where(o => o.PackageName.ToLower().Contains(packageParameter.PackageName.ToLower()));
            }

            // 3. Execute Query 
            return await items
                .Include(e => e.EquipmentSets)
                .ThenInclude(eq => eq.Equipment)
                    .ThenInclude(c => c.Category)
                .ToListAsync();
        }

        public async Task<Package?> GetPackageByIdAsync(int id,bool trackchange)
        {
            return await FindByCondition(e => e.PackageId == id, trackchange)
                .Include(e => e.EquipmentSets)
                .ThenInclude(eq => eq.Equipment)
                    .ThenInclude(c => c.Category)
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
