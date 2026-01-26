using Entities.Models;
using Service.Contracts.DTOs.Outsource;

namespace Service.Contracts.IService
{
    public interface IOutsourceService
    {
        Task<IEnumerable<OutsourceDto>> GetOutsources(string? fullName);
        Task<OutsourceDto?> GetOutsourcesByIdAsync(int id);
        Task<OutsourceDto> CreateOutsourceAsync(CreateOutsourceDto dto);
        Task<OutsourceDto> UpdateOutsourceAsync(int id, UpdateOutsourceDto dto);
        Task DeleteOutsourceAsync(int id);
    }
}
