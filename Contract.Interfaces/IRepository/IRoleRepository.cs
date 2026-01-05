using Contract.Interfaces.IRepository.BaseManager;
using Entity.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Interfaces.IRepository
{
    public interface IRoleRepository : IRepositoryBase<Role>
    {
        Task<IEnumerable<Role>> GetAllRoleAsync();
    }
}
