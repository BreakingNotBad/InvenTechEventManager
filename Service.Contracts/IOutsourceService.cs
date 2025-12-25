using Entity.Domain.Model;

namespace Service.Contract
{
    public interface IOutsourceService
    {
        Task<IEnumerable<Outsource>> GetOutsources();
        Task<Outsource?> GetOutsourcesByIdAsync(int id);
        Task DeleteOutsource(int id);
    }
}
