using Entity.Domain.Model;

namespace Contract.Interfaces.IRepository
{
    public interface IOutsourceRepository : IRepositoryBase<Outsources>
    {
        Task<IEnumerable<Outsources>> GetOutsourceAsyn();
        Task<Outsources?> GetOutsourceByIdAsync(int id);
        Task<IEnumerable<Outsources>> GetOutsourceActiveAsync(
            string? search,
            DateOnly? date,
            string? time_period,
            Boolean? filter_available
        );
        void Createoutsource(Outsources outsource);
        void Updateoutsource(Outsources outsource);
        void Deleteoutsource(Outsources outsource);
    }
}
