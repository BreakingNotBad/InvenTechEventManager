using Contract.Interfaces.IRepository;
using Entity.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Repository.Infrastructure.Data;
using Repository.Repositories;

namespace Repository.Infrastructure.Repository
{
    public class RoleRepository : RepositoryBase<Role>, IRoleRepository
    {
        public RoleRepository(RepositoryContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Role>> GetRolesAsync()
        {
            return await FindAll(trackChanges: false).ToListAsync();
        }

        public async Task<Role?> GetRoleByIdAsync(int id)
        {
            return await FindByCondition(e => e.RoleId == id, trackChanges: false)
                .FirstOrDefaultAsync();
        }
    }
}
