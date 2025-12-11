using Entity.Domain.Model;

namespace Contract.Interfaces.IRepository
{
    public interface IRoleRepository : IRepositoryBase<Role>
    {
        Task<IEnumerable<Role>> GetRolesAsync();
        Task<Role?> GetRoleByIdAsync(int id);
    }
}
