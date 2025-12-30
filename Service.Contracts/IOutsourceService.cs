using Entity.Domain.Model;

namespace Service.Contract
{
    public interface IOutsourceService
    {
        Task<IEnumerable<Outsource>> GetOutsources();
        Task<Outsource?> GetOutsourcesByIdAsync(int id);
        Task CreateOutsourceAsync(Outsource outsource);
        Task UpdateOutsourceAsync(int id, Outsource outsource);
        Task DeleteOutsourceAsync(int id);
    }
}
