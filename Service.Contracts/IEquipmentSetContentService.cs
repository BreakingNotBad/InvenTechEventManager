using Entity.Domain.Model;

namespace Service.Contract
{
    public interface IEquipmentSetContentService
    {
        Task<IEnumerable<EquipmentSetContent>> GetEquipmentSetContentsAsync();
        Task<IEnumerable<EquipmentSetContent>> GetContentsByEquipmentSetIdAsync(int equipmentSetId);
    }
}
