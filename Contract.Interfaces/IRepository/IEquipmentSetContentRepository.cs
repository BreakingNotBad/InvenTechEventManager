using Entity.Domain.Model;

namespace Contract.Interfaces.IRepository
{
    public interface IEquipmentSetContentRepository : IRepositoryBase<EquipmentSetContent>
    {
        Task<IEnumerable<EquipmentSetContent>> GetEquipmentSetContentsAsync();
        Task<IEnumerable<EquipmentSetContent>> GetContentsByEquipmentSetIdAsync(int equipmentSetId);

    }
}
