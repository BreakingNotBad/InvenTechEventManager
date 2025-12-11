using Contract.Interfaces.IRepository;
using Entity.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Repository.Infrastructure.Data;
using Repository.Repositories;

namespace Repository.Infrastructure.Repository
{
    public class UserPermissionRepository : RepositoryBase<UserPermission>, IUserPermissionRepository
    {
        public UserPermissionRepository(RepositoryContext context) : base(context)
        {
        }
        public async Task<IEnumerable<UserPermission>> GetUserPermissionsAsync()
        {
            return await FindAll(trackChanges: false)
                .ToListAsync();
        }
        public async Task<IEnumerable<UserPermission>> GetByUserIdAsync(int userId)
        {
            return await FindByCondition(up => up.UserId == userId, trackChanges: false)
                .ToListAsync();
        }
    }
}
