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
    }
}
