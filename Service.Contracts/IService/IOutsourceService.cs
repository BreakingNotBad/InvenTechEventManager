using Entities.Models;
using Service.Contracts.DTOs.Outsource;

namespace Service.Contracts.IService
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
