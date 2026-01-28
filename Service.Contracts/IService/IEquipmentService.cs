using Entities.Models;
using Service.Contracts.DTOs.Equipment;
using Shared.RequestFeatures.Parameters;

namespace Service.Contracts.IService
{
    public interface IEquipmentService
    {
        Task<IEnumerable<EquipmentDto>> GetEquipmentAsync(
            EquipmentParameter equipmentParameter);
        Task<EquipmentDto?> GetEquipmentByIdAsync(int id);
        Task <EquipmentDto>CreateEquipmentAsync(CreateEquipmentDto dto);
        Task <EquipmentDto> UpdateEquipmentAsync(int id, UpdateEquipmentDto dto);
        Task DeleteEquipment(int id);
    }
}
