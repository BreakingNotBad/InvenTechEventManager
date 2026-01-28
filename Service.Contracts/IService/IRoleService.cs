using Entities.Models;
using Service.Contracts.DTOs.Role;
using Shared.RequestFeatures.Parameters;

namespace Service.Contracts.IService
{
    public interface IRoleService
    {
        Task<IEnumerable<RoleDto>> GetRoleByAsync(RoleParameter roleParameter);
    }
}
