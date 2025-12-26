using Entity.Domain.Model;

namespace Service.Contract
{
    public interface IOutsourceService
    {
        Task<IEnumerable<Outsources>> GetOutsources();
        Task<Outsources?> GetOutsourcesByIdAsync(int id);
        Task<IEnumerable<Outsources>> GetOutsourceActiveAsync(
            string? search,
            DateOnly? date,
            string? time_period,
            Boolean? filter_available
        );
        Task CreateOutsourceAsync(Outsources outsource);
        Task UpdateOutsourceAsync(int id, Outsources outsource);
        Task DeleteOutsource(int id);
    }
}
