using Contract.Interfaces.IRepository;
using Entity.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Repository.Infrastructure.Data;

namespace Repository.Infrastructure.Repository
{
    public class EventRepository : IEventRepository
    {
        private readonly RepositoryContext _context;

        public EventRepository(RepositoryContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Event>> GetAllAsync()
        {
            // ดึงทุก event จาก DB
            return await _context.Events.ToListAsync();
        }

        public async Task<Event?> GetByIdAsync(int id)
        {
            return await _context.Events
                .FirstOrDefaultAsync(e => e.EventId == id);
        }
    }
}