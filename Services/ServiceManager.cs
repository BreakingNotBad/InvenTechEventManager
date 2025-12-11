using Contract.Interfaces.IRepository;
using Service.Contract;

namespace Service
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<IEventService> _eventService;
        private readonly Lazy<ICompanyService> _companyService;
        private readonly Lazy<ICompanyContactService> _companyContactService;
        private readonly Lazy<IEquipmentService> _equipmentService;
        private readonly Lazy<IEquipmentSetContentService> _equipmentSetContentService;
        private readonly Lazy<IEquipmentSetService> _equipmentSetService;
        private readonly Lazy<IEventEquipmentService> _eventEquipmentService;
        private readonly Lazy<IEventGuestService> _eventGuestService;
        private readonly Lazy<IEventStaffService> _eventStaffService;
        private readonly Lazy<IGuestUserService> _guestUserService;
        private readonly Lazy<IPackageService> _packageService;
        private readonly Lazy<IPermissionService> _permissionService;
        private readonly Lazy<IQuoteStatusService> _quoteStatusService;
        private readonly Lazy<IRoleService> _roleService;
        private readonly Lazy<IUserPermissionService> _userPermissionService;
        private readonly Lazy<IUserService> _userService;

        public ServiceManager(IRepositoryManager repo)
        {
            _eventService = new Lazy<IEventService>(() => new EventService(repo));
            _companyService = new Lazy<ICompanyService>(() => new CompanyService(repo));
            _companyContactService = new Lazy<ICompanyContactService>(() => new CompanyContactService(repo));
            _equipmentService = new Lazy<IEquipmentService>(() => new EquipmentService(repo));
            _equipmentSetContentService = new Lazy<IEquipmentSetContentService>(() => new EquipmentSetContentService(repo));
            _equipmentSetService = new Lazy<IEquipmentSetService>(() => new EquipmentSetService(repo));
            _eventEquipmentService = new Lazy<IEventEquipmentService>(() => new EventEquipmentService(repo));
            _eventGuestService = new Lazy<IEventGuestService>(() => new EventGuestService(repo));
            _eventStaffService = new Lazy<IEventStaffService>(() => new EventStaffService(repo));
            _guestUserService = new Lazy<IGuestUserService>(() => new GuestUserService(repo));
            _packageService = new Lazy<IPackageService>(() => new PackageService(repo));
            _permissionService = new Lazy<IPermissionService>(() => new PermissionService(repo));
            _quoteStatusService = new Lazy<IQuoteStatusService>(() => new QuoteStatusService(repo));
            _roleService = new Lazy<IRoleService>(() => new RoleService(repo));
            _userPermissionService = new Lazy<IUserPermissionService>(() => new UserPermissionService(repo));
            _userService = new Lazy<IUserService>(() => new UserService(repo));
        }

        public IEventService Event => _eventService.Value;
        public ICompanyService Company => _companyService.Value;
        public ICompanyContactService CompanyContact => _companyContactService.Value;
        public IEquipmentService Equipment => _equipmentService.Value;
        public IEquipmentSetContentService EquipmentSetContent => _equipmentSetContentService.Value;
        public IEquipmentSetService EquipmentSet => _equipmentSetService.Value;
        public IEventEquipmentService EventEquipment => _eventEquipmentService.Value;
        public IEventGuestService EventGuest => _eventGuestService.Value;
        public IEventStaffService EventStaff => _eventStaffService.Value;
        public IGuestUserService GuestUser => _guestUserService.Value;
        public IPackageService Package => _packageService.Value;
        public IPermissionService Permission => _permissionService.Value;
        public IQuoteStatusService QuoteStatus => _quoteStatusService.Value;
        public IRoleService Role => _roleService.Value;
        public IUserPermissionService UserPermission => _userPermissionService.Value;
        public IUserService User => _userService.Value;
    }
}
