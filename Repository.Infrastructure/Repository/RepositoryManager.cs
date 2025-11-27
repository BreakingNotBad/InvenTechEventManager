using Contact.Interfaces.IRepository;
using Repository.Infrastructure.Data;

namespace Repository.Infrastructure.Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoryContext _context;
        private readonly Lazy<IEventRepository> _eventRepository;

        public RepositoryManager(RepositoryContext context)
        {
            _context = context;
            _eventRepository = new Lazy<IEventRepository>(() => new EventRepository(_context));
        }

        // property สำหรับเรียกใช้ EventRepository
        public IEventRepository Event => _eventRepository.Value;

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}