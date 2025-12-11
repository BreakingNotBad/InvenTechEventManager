using Entity.Domain.Model;

namespace Service.Contract
{
    public interface IEquipmentSetService
    {
        Task<IEnumerable<EquipmentSet>> GetEquipmentSetsAsync();
        Task<EquipmentSet?> GetEquipmentSetByIdAsync(int id);
    }
}
