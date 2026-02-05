using Contracts.IRepository.BaseManager;
using Entities.Models;
using Shared.RequestFeatures.Parameters;

namespace Contracts.IRepository
{
    public interface IRoleRepository : IRepositoryBase<Role>
    {
        Task<IEnumerable<Role>> GetAllRoleAsync(RoleParameter roleParameter, bool trackChanges);
        Task<bool> RoleExistsAsync(List<int> roleId, bool trackChanges);
        Task<bool> AllRoleIdsExistAsync(IEnumerable<int> roleIds);
    }
}
