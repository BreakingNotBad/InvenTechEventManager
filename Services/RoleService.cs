using Contract.Interfaces.IRepository.BaseManager;
using Entity.Domain.Model;
using Service.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class RoleService: IRoleService
    {
        private readonly IRepositoryManager _repo;
        public RoleService (IRepositoryManager repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<Role>> GetRoleByAsync()
        {
           return await _repo.Role.GetAllRoleAsync();
        }
    }
}
