using Entities.Models;
using Service.Contracts.DTOs.Equipment;

namespace Service.Contracts.IService
{
    public interface IEquipmentService
    {
        Task<IEnumerable<EquipmentDto>> GetEquipmentAsync(
            string? equipmentName,
            string? category,
            bool? IsDeleted,
            DateTime CreatedAt,
            DateTime UpdatedAt);
        Task<EquipmentDto?> GetEquipmentByIdAsync(int id);
        Task <EquipmentDto>CreateEquipmentAsync(CreateEquipmentDto dto);
        Task <EquipmentDto> UpdateEquipmentAsync(int id, UpdateEquipmentDto dto);
        Task DeleteEquipment(int id);
    }
}
