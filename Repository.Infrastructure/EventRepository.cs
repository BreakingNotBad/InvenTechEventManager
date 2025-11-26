using Contract.Interfaces.Model;
using Entity.Domain.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Infrastructure
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

