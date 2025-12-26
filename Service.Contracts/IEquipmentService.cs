using Entity.Domain.Model;

namespace Service.Contract
{
    public interface IEquipmentService
    {
        Task<IEnumerable<Equipments>> GetEquipmentAsync();
        Task<Equipments?> GetEquipmentByIdAsync(int id);
        Task CreateEquipmentAsync(Equipments equipment);
        Task DeleteEquipment(int id);

    }
}
