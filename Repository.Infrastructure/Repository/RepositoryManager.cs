using Contract.Interfaces.IRepository;
using Repository.Infrastructure.Data;

namespace Repository.Infrastructure.Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoryContext _context;
        private readonly Lazy<IEventRepository> _eventRepo;

        public RepositoryManager(RepositoryContext context)
        {
            _context = context;
            _eventRepo = new Lazy<IEventRepository>(() => new EventRepository(context));
        }

        public IEventRepository Event => _eventRepo.Value;

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}