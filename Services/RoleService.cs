using Contracts.IRepository.BaseManager;
using Entities.Models;
using Service.Contracts;

namespace Service
{
    public class RoleService : IRoleService
    {
        private readonly IRepositoryManager _repo;

        public RoleService(IRepositoryManager repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<Role>> GetRoleByAsync()
        {
            return await _repo.Role.GetAllRoleAsync();
        }
    }
}
