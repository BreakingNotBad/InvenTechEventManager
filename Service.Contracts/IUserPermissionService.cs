using Entity.Domain.Model;

namespace Service.Contract
{
    public interface IUserPermissionService
    {
        Task<IEnumerable<UserPermission>> GetUserPermissionsAsync();
        Task<IEnumerable<UserPermission>> GetByUserIdAsync(int userId);
    }
}
