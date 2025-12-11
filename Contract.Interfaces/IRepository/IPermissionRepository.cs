using Entity.Domain.Model;

namespace Contract.Interfaces.IRepository
{
    public interface IPermissionRepository : IRepositoryBase<Permission>
    {
        Task<IEnumerable<Permission>> GetPermissionsAsync();
        Task<Permission?> GetPermissionByIdAsync(int id);
    }
}
