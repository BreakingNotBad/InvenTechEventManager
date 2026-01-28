using Contracts.IRepository;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Data;
using Repositories.Repository.BaseManager;
using Shared.RequestFeatures.Parameters;

namespace Repositories.Repository
{
    public class RoleRepository : RepositoryBase<Role>, IRoleRepository
    {
        public RoleRepository(RepositoryContext context)
            : base(context) { }

        public async Task<IEnumerable<Role>> GetAllRoleAsync(RoleParameter roleParameter, bool trackChanges)
        {
            // 1. เริ่มต้น Query
            var items = FindAll(trackChanges);

            // 2. Filter: Search by RoleName
            if (!string.IsNullOrWhiteSpace(roleParameter.RoleName))
            {
                items = items.Where(o => o.RoleName.ToLower().Contains(roleParameter.RoleName.ToLower()));
            }
            return await items.ToListAsync();
        }

        public async Task<bool> RoleExistsAsync(List<int> roleId, bool trackChanges)
        {
            if (roleId == null || roleId.Count == 0)
            {
                return false;
            }
            var uniqueRoleId = roleId.Distinct().ToList(); // Distinct เอาเฉพาะ RoleId ที่ไม่ซ้ำกัน

            var count = await FindByCondition(r => uniqueRoleId.Contains(r.RoleId), trackChanges)
                .CountAsync();
            return count == uniqueRoleId.Count;
        }
    }
}
