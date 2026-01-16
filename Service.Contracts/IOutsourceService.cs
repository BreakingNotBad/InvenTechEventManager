using Contract.Interfaces.DTOs;
using Entity.Domain.Model;

namespace Service.Contract
{
    public interface IOutsourceService
    {
        Task<IEnumerable<Outsource>> GetOutsources(string? fullName);
        Task<Outsource?> GetOutsourcesByIdAsync(int id);
        Task<Outsource> CreateOutsourceAsync(CreateOutsourceDto dto);
        Task UpdateOutsourceAsync(int id, UpdateOutsourceDto dto);
        Task DeleteOutsourceAsync(int id);
    }
}
