using Entity.Domain.Model;

namespace Service.Contract
{
    public interface IOutsourceService
    {
        Task<IEnumerable<Outsource>> GetOutsources();
        Task<Outsource?> GetOutsourcesByIdAsync(int id);
        Task<IEnumerable<Outsource>> GetOutsourceActiveAsync(
            string? search,
            DateOnly? date,
            string? time_period,
            Boolean? filter_available
        );
        Task CreateOutsourceAsync(Outsource outsource);
        Task UpdateOutsourceAsync(int id, Outsource outsource);
        Task DeleteOutsource(int id);
    }
}
