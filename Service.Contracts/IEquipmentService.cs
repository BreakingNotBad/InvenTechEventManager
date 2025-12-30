using Entity.Domain.Model;

namespace Service.Contract
{
    public interface IEquipmentService
    {
        Task<IEnumerable<Equipment>> GetEquipmentAsync();
        Task<Equipment?> GetEquipmentByIdAsync(int id);
        Task CreateEquipmentAsync(Equipment equipment);
        Task UpdateEquipmentAsync(int id, Equipment equipment);
        Task DeleteEquipment(int id);
    }
}
