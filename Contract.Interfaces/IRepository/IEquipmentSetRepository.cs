using Entity.Domain.Model;

namespace Contract.Interfaces.IRepository
{
    public interface IEquipmentSetRepository : IRepositoryBase<EquipmentSet>
    {
        Task<IEnumerable<EquipmentSet>> GetEquipmentSetsAsync();
        Task<EquipmentSet?> GetEquipmentSetByIdAsync(int id);
    }
}
