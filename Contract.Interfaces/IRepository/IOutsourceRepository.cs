using Entity.Domain.Model;

namespace Contract.Interfaces.IRepository
{
    public interface IOutsourceRepository : IRepositoryBase<Outsource>
    {
        Task<IEnumerable<Outsource>> GetOutsourceAsyn();
        Task<Outsource?> GetOutsourceByIdAsync(int id);
        Task Deleteoutsource(Outsource outsource);
    }
}
