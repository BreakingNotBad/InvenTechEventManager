using Entity.Domain.Model;

namespace Contract.Interfaces.IRepository
{
    public interface IUserPermissionRepository : IRepositoryBase<UserPermission>
    {
        Task<IEnumerable<UserPermission>> GetUserPermissionsAsync();
        Task<IEnumerable<UserPermission>> GetByUserIdAsync(int userId);

    }
}
