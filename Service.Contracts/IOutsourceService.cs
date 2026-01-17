using Contracts.DTOs;
using Entities.Models;

namespace Service.Contracts
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
