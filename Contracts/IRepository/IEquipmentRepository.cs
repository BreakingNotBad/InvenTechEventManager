using Contracts.IRepository.BaseManager;
using Entities.Models;
using Shared.RequestFeatures.Parameters;

namespace Contracts.IRepository
{
    public interface IEquipmentRepository : IRepositoryBase<Equipment>
    {
        Task<IEnumerable<Equipment>> GetEquipmentAsync(EquipmentParameter equipmentParameter ,bool trackChanges);
        Task<Equipment?> GetEquipmentByIdAsync(int id, bool trackchange);
        void CreateEquipment(Equipment equipment);
        void UpdateEquipment(Equipment equipment);
        void DeleteEquipment(Equipment equipment);
    }
}
