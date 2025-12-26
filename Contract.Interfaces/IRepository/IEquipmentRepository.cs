using Entity.Domain.Model;

namespace Contract.Interfaces.IRepository
{
    public interface IEquipmentRepository : IRepositoryBase<Equipments>
    {
        Task<IEnumerable<Equipments>> GetEquipmentAsync();
        Task<Equipments?> GetEquipmentByIdAsync(int id);
        void CreateEquipment (Equipments equipment);
        void DeleteEquipment(Equipments equipment);
    }
}
