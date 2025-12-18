using Contract.Interfaces.IRepository;
using Entity.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Service.Contract;

namespace Service
{
    public class PermissionService : IPermissionService
    {
        private readonly IRepositoryManager _repo;

        public PermissionService(IRepositoryManager repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<Permission>> GetPermissionsAsync()
        {
            return await _repo.Permission.GetPermissionsAsync();
        }

        public async Task<Permission?> GetPermissionByIdAsync(int id)
        {
            return await _repo.Permission.GetPermissionByIdAsync(id);
        }
    }
}
