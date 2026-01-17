using Entities.Models;

namespace Service.Contracts
{
    public interface IEquipmentService
    {
        Task<IEnumerable<Equipment>> GetEquipmentAsync(string? equipmentName, string? category);
        Task<Equipment?> GetEquipmentByIdAsync(int id);
        Task CreateEquipmentAsync(Equipment equipment);
        Task UpdateEquipmentAsync(int id, Equipment equipment);
        Task DeleteEquipment(int id);
    }
}
