using Entity.Domain.Model;

namespace Contract.Interfaces.IRepository
{
    public interface IEventEquipmentRepository : IRepositoryBase<EventEquipment>
    {
        Task<IEnumerable<EventEquipment>> GetEventEquipmentsAsync();
        Task<IEnumerable<EventEquipment>> GetByEventIdAsync(int eventId);

    }
}
