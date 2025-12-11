using Contract.Interfaces.IRepository;
using Entity.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Service.Contract;

namespace Service
{
    public class EventEquipmentService : IEventEquipmentService
    {
        private readonly IRepositoryManager _repo;

        public EventEquipmentService(IRepositoryManager repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<EventEquipment>> GetEventEquipmentsAsync()
        {
            return await _repo.EventEquipment.GetEventEquipmentsAsync();
        }

        public async Task<IEnumerable<EventEquipment>> GetByEventIdAsync(int eventId)
        {
            return await _repo.EventEquipment.FindByCondition(
                e => e.EventId == eventId,
                trackChanges: false
            ).ToListAsync();
        }
    }
}
