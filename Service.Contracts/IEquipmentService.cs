using Entity.Domain.Model;

namespace Service.Contract
{
    public interface IEquipmentService
    {
        Task<IEnumerable<Equipment>> GetEquipmentAsync();
        Task<Equipment?> GetEquipmentByIdAsync(int id);

    }
}
