using Entity.Domain.Model;

namespace Service.Contract
{
    public interface IOutsourceService
    {
        Task<IEnumerable<Outsource>> GetOutsources();
        Task CreateOutsourceAsync(Outsource outsource);
        Task<Outsource?> GetOutsourcesByIdAsync(int id);
        Task UpdateOutsourceAsync(int id, Outsource outsource);
        Task DeleteOutsource(int id);
    }
}
