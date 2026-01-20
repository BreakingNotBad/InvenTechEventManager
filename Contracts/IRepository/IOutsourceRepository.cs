using Contracts.IRepository.BaseManager;
using Entities.Models;

namespace Contracts.IRepository
{
    public interface IOutsourceRepository : IRepositoryBase<Outsource>
    {
        Task<IEnumerable<Outsource>> GetOutsourceAsyn();
        Task<Outsource?> GetOutsourceByIdAsync(int id, bool trackchange);
        void CreateOutsource(Outsource outsource);
        void UpdateOutsource(Outsource outsource);
        void DeleteOutsource(Outsource outsource);
    }
}
