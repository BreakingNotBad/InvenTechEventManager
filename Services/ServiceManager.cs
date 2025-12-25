using Contract.Interfaces.IRepository;
using Service.Contract;

namespace Service
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<IEventService> _eventService;
        private readonly Lazy<ICompanyService> _companyService;
        private readonly Lazy<IStaffService> _staffService;
        private readonly Lazy<IOutsourceService> _outsourceService;
        private readonly Lazy<IEquipmentService> _equipmentService;
        private readonly Lazy<IPackageService> _packageService;

        public ServiceManager(IRepositoryManager repo)
        {
            _eventService = new Lazy<IEventService>(() => new EventService(repo));
            _companyService = new Lazy<ICompanyService>(() => new CompanyService(repo));
            _staffService = new Lazy<IStaffService>(() => new StaffService(repo));
            _outsourceService = new Lazy<IOutsourceService>(() => new OutsourceService(repo));
            _equipmentService = new Lazy<IEquipmentService>(() => new EquipmentService(repo));
            _packageService = new Lazy<IPackageService>(() => new PackageService(repo));
        }

        public IEventService Event => _eventService.Value;
        public ICompanyService Company => _companyService.Value;
        public IStaffService Staff => _staffService.Value;
        public IOutsourceService Outsource => _outsourceService.Value;
        public IEquipmentService Equipment => _equipmentService.Value;
        public IPackageService Package => _packageService.Value;
    }
}
