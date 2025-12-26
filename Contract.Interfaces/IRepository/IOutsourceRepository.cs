using Entity.Domain.Model;

namespace Contract.Interfaces.IRepository
{
    public interface IOutsourceRepository : IRepositoryBase<Outsource>
    {
        Task<IEnumerable<Outsource>> GetOutsourceAsyn();
        Task<Outsource?> GetOutsourceByIdAsync(int id);
        Task<IEnumerable<Outsource>> GetOutsourceActiveAsync(
            string? search,
            DateOnly? date,
            string? time_period,
            Boolean? filter_available
        );
        void Createoutsource(Outsource outsource);
        void Updateoutsource(Outsource outsource);
        void Deleteoutsource(Outsource outsource);
    }
}
