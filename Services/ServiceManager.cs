using Contract.Interfaces.IRepository;
using Service.Contract;

namespace Service
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<IEventService> _eventService;

        public ServiceManager(IRepositoryManager repo)
        {
            _eventService = new Lazy<IEventService>(() => new EventService(repo));
        }

        public IEventService Event => _eventService.Value;
    }
}
