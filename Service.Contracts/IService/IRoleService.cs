using Entities.Models;

namespace Service.Contracts.IService
{
    public interface IRoleService
    {
        Task<IEnumerable<Role>> GetRoleByAsync();
    }
}
