using Contracts.IRepository.BaseManager;
using Entities.Models;
using Shared.RequestFeatures.Parameters;

namespace Contracts.IRepository
{
    public interface IOutsourceRepository : IRepositoryBase<Outsource>
    {
        Task<IEnumerable<Outsource>> GetOutsourceAsyn(OutsourceParameter outsourceParameter, bool trackChanges);
        Task<Outsource?> GetOutsourceByIdAsync(int id, bool trackchange);
        void CreateOutsource(Outsource outsource);
        void UpdateOutsource(Outsource outsource);
        void DeleteOutsource(Outsource outsource);
    }
}
