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
            var query = _repo.Permission.FindByCondition(
                e => e.PermissionId == id,
                trackChanges: false
            );

            return await query.FirstOrDefaultAsync();
        }
    }
}
