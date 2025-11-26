using Contract.Interfaces;
using Contract.Interfaces.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Infrastructure
{
    public class RepositoryManager :IRepositoryManager
    {
        private readonly RepositoryContext _context;

        private IEventRepository? _eventRepository;

        public RepositoryManager(RepositoryContext context)
        {
            _context = context;
        }

        // property สำหรับเรียกใช้ EventRepository
        public IEventRepository Event
            => _eventRepository ??= new EventRepository(_context);

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
