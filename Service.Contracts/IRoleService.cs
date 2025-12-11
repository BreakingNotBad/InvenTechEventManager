using Entity.Domain.Model;

namespace Service.Contract
{
    public interface IRoleService
    {
        Task<IEnumerable<Role>> GetRolesAsync();
        Task<Role?> GetRoleByIdAsync(int id);
    }
}
