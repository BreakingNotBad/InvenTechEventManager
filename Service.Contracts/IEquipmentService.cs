using Entity.Domain.Model;

namespace Service.Contract
{
    public interface IEquipmentService
    {
        Task<IEnumerable<Equipment>> GetEquipmentAsync(
            string? query,
            string? category);
        Task<Equipment?> GetEquipmentByIdAsync(int id);
        Task CreateEquipmentAsync(Equipment equipment);
        Task UpdateEquipmentAsync(int id, Equipment equipment);
        Task DeleteEquipment(int id);
    }
}
