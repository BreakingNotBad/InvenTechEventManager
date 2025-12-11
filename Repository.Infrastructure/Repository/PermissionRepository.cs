using Contract.Interfaces.IRepository;
using Entity.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Repository.Infrastructure.Data;
using Repository.Repositories;

namespace Repository.Infrastructure.Repository
{
    public class PermissionRepository : RepositoryBase<Permission>, IPermissionRepository
    {
        public PermissionRepository(RepositoryContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Permission>> GetPermissionsAsync()
        {
            return await FindAll(trackChanges: false).ToListAsync();
        }

        public async Task<Permission?> GetPermissionByIdAsync(int id)
        {
            return await FindByCondition(e => e.PermissionId == id, trackChanges: false)
                .FirstOrDefaultAsync();
        }
    }
}
