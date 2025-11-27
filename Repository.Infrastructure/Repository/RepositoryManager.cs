using Contract.Interfaces.IRepository;
using Repository.Infrastructure.Data;

namespace Repository.Infrastructure.Repository
{
    public class RepositoryManager : IRepositoryManager
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