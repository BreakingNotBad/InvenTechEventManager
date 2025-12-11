using Entity.Domain.Model;

namespace Service.Contract
{
    public interface IPermissionService
    {
        Task<IEnumerable<Permission>> GetPermissionsAsync();
        Task<Permission?> GetPermissionByIdAsync(int id);
    }
}
