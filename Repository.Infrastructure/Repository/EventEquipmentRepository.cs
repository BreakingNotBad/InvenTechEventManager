using Contract.Interfaces.IRepository;
using Entity.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Repository.Infrastructure.Data;
using Repository.Repositories;

namespace Repository.Infrastructure.Repository
{
    public class EventEquipmentRepository : RepositoryBase<EventEquipment>, IEventEquipmentRepository
    {
        public EventEquipmentRepository(RepositoryContext context) : base(context)
        {
        }
        public async Task<IEnumerable<EventEquipment>> GetEventEquipmentsAsync()
        {
            return await FindAll(trackChanges: false)
                .ToListAsync();
        }

        public async Task<IEnumerable<EventEquipment>> GetByEventIdAsync(int eventId)
        {
            return await FindByCondition(e => e.EventId == eventId, trackChanges: false)
                .ToListAsync();
        }
    }
}
