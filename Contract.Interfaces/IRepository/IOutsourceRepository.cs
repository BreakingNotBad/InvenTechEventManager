using Contract.Interfaces.IRepository.BaseManager;
using Entity.Domain.Model;

namespace Contract.Interfaces.IRepository
{
    public interface IOutsourceRepository : IRepositoryBase<Outsource>
    {
        Task<IEnumerable<Outsource>> GetOutsourceAsyn();
        Task<Outsource?> GetOutsourceByIdAsync(int id);
        void CreateOutsource(Outsource outsource);
        void UpdateOutsource(Outsource outsource);
        void DeleteOutsource(Outsource outsource);
    }
}
