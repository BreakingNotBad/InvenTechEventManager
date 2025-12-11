using Contract.Interfaces.IRepository;
using Entity.Domain.Model;
using Service.Contract;
using Microsoft.EntityFrameworkCore;

namespace Service
{
    public class EventService : IEventService
    {
        private readonly IRepositoryManager _repo;
        public EventService(IRepositoryManager repo) 
        {
            _repo = repo;
        }

        public async Task<IEnumerable<Event>> GetEventsAsync()
        {
            return await _repo.Event.GetEventsAsync();
        }

        public async Task<Event?> GetEventByIdAsync(int id)
        {
            // ดึง IQueryable จาก base repo
            var query = _repo.Event.FindByCondition(
                e => e.EventId == id,      // หรือ e.EventID ตามชื่อจริงใน model
                trackChanges: false        // อ่านอย่างเดียว ไม่ต้อง track
            );

            // ใช้ EF Core extension ทำให้กลายเป็นตัวเดียว
            return await query.FirstOrDefaultAsync();
        }
    }
}
