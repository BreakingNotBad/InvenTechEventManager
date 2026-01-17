using Entities.Models;

namespace Service.Contracts
{
    public interface IRoleService
    {
        Task<IEnumerable<Role>> GetRoleByAsync();
    }
}
