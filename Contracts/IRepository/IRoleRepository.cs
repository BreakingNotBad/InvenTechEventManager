using Contracts.IRepository.BaseManager;
using Entities.Models;

namespace Contracts.IRepository
{
    public interface IRoleRepository : IRepositoryBase<Role>
    {
        Task<IEnumerable<Role>> GetAllRoleAsync();
        Task<bool> RoleExistsAsync(List<int> roleId, bool trackChanges);
    }
}
