using Contracts.DTOs;
using Entities.Models;

namespace Service.Contracts
{
    public interface IEquipmentService
    {
        Task<IEnumerable<Equipment>> GetEquipmentAsync(
            string? equipmentName,
            string? category,
            bool? IsDeleted,
            DateTime CreatedAt,
            DateTime UpdatedAt);
        Task<Equipment?> GetEquipmentByIdAsync(int id);
        Task <Equipment>CreateEquipmentAsync(CreateEquipmentDto dto);
        Task <Equipment>UpdateEquipmentAsync(int id, UpdateEquipmentDto dto);
        Task DeleteEquipment(int id);
    }
}
