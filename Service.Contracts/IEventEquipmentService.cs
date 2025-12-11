using Entity.Domain.Model;

namespace Service.Contract
{
    public interface IEventEquipmentService
    {
        Task<IEnumerable<EventEquipment>> GetEventEquipmentsAsync();
        Task<IEnumerable<EventEquipment>> GetByEventIdAsync(int eventId);
    }
}
