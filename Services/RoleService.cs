using Contract.Interfaces.IRepository;
using Entity.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Service.Contract;

namespace Service
{
    public class RoleService : IRoleService
    {
        private readonly IRepositoryManager _repo;

        public RoleService(IRepositoryManager repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<Role>> GetRolesAsync()
        {
            return await _repo.Role.GetRolesAsync();
        }

        public async Task<Role?> GetRoleByIdAsync(int id)
        {
            var query = _repo.Role.FindByCondition(
                e => e.RoleId == id,
                trackChanges: false
            );

            return await query.FirstOrDefaultAsync();
        }
    }
}
