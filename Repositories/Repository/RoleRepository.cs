using Contracts.IRepository;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Data;
using Repositories.Repository.BaseManager;

namespace Repositories.Repository
{
    public class RoleRepository : RepositoryBase<Role>, IRoleRepository
    {
        public RoleRepository(RepositoryContext context)
            : base(context) { }

        public async Task<IEnumerable<Role>> GetAllRoleAsync()
        {
            return await FindAll(trackChanges: false).ToListAsync();
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
