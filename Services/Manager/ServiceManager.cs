using Contract.Interfaces.IRepository.BaseManager;
using Service.Contract;
using Service.Contract.Manager;

namespace Service.Manager
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<IEventService> _eventService;
        private readonly Lazy<ICompanyService> _companyService;
        private readonly Lazy<IStaffService> _staffService;
        private readonly Lazy<IOutsourceService> _outsourceService;
        private readonly Lazy<IEquipmentService> _equipmentService;
        private readonly Lazy<IPackageService> _packageService;
        private readonly Lazy<IRoleService> _roleService;
        private readonly Lazy<ICategoryService> _categoryService;

        public ServiceManager(IRepositoryManager repo)
        {
            _eventService = new Lazy<IEventService>(() => new EventService(repo));
            _companyService = new Lazy<ICompanyService>(() => new CompanyService(repo));
            _staffService = new Lazy<IStaffService>(() => new StaffService(repo));
            _outsourceService = new Lazy<IOutsourceService>(() => new OutsourceService(repo));
            _equipmentService = new Lazy<IEquipmentService>(() => new EquipmentService(repo));
            _packageService = new Lazy<IPackageService>(() => new PackageService(repo));
            _roleService = new Lazy<IRoleService>(() => new RoleService(repo));
            _categoryService = new Lazy<ICategoryService>(() => new CategoryService(repo));
        }

        public IEventService Event => _eventService.Value;
        public ICompanyService Company => _companyService.Value;
        public IStaffService Staff => _staffService.Value;
        public IOutsourceService Outsource => _outsourceService.Value;
        public IEquipmentService Equipment => _equipmentService.Value;
        public IPackageService Package => _packageService.Value;
        public IRoleService Role => _roleService.Value;
        public ICategoryService Category => _categoryService.Value;
    }
}
