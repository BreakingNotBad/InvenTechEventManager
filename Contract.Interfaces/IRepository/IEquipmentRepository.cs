using Entity.Domain.Model;

namespace Contract.Interfaces.IRepository
{
    public interface IEquipmentRepository : IRepositoryBase<Equipment>
    {
        Task<IEnumerable<Equipment>> GetEquipmentAsync();
        Task<Equipment?> GetEquipmentByIdAsync(int id);
    }
}
