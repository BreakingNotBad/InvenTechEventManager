using Contract.Interfaces.IRepository;
using Repository.Infrastructure.Data;

namespace Repository.Infrastructure.Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoryContext _context;
        private readonly Lazy<IEventRepository> _eventRepo;
        private readonly Lazy<ICompanyRepository> _companyRepo;
        private readonly Lazy<ICompanyContactRepository> _companyContactRepo;
        private readonly Lazy<IEquipmentRepository> _equipmentRepo;
        private readonly Lazy<IEquipmentSetContentRepository> _equipmentSetContentRepo;
        private readonly Lazy<IEquipmentSetRepository> _equipmentSetRepo;
        private readonly Lazy<IEventEquipmentRepository> _eventEquipmentRepo;
        private readonly Lazy<IEventGuestRepository> _eventGuestRepo;
        private readonly Lazy<IEventStaffRepository> _eventStaffRepo;
        private readonly Lazy<IGuestUserRepository> _guestUserRepo;
        private readonly Lazy<IPackageRepository> _packageRepo;
        private readonly Lazy<IPermissionRepository> _permissionRepo;
        private readonly Lazy<IQuoteStatusRepository> _quoteStatusRepo;
        private readonly Lazy<IRoleRepository> _roleRepo;
        private readonly Lazy<IUserPermissionRepository> _userPermissionRepo;
        private readonly Lazy<IUserRepository> _userRepo;

        public RepositoryManager(RepositoryContext context)
        {
            _context = context;
            _eventRepo = new Lazy<IEventRepository>(() => new EventRepository(context));
            _companyRepo = new Lazy<ICompanyRepository>(() => new CompanyRepository(context));
            _companyContactRepo = new Lazy<ICompanyContactRepository>(() => new CompanyContactRepository(context));
            _equipmentRepo = new Lazy<IEquipmentRepository>(() => new EquipmentRepository(context));
            _equipmentSetContentRepo = new Lazy<IEquipmentSetContentRepository>(() => new EquipmentSetContentRepository(context));
            _equipmentSetRepo = new Lazy<IEquipmentSetRepository>(() => new EquipmentSetRepository(context));
            _eventEquipmentRepo = new Lazy<IEventEquipmentRepository>(() => new EventEquipmentRepository(context));
            _eventGuestRepo = new Lazy<IEventGuestRepository>(() => new EventGuestRepository(context));
            _eventStaffRepo = new Lazy<IEventStaffRepository>(() => new EventStaffRepository(context));
            _guestUserRepo = new Lazy<IGuestUserRepository>(() => new GuestUserRepository(context));
            _packageRepo = new Lazy<IPackageRepository>(() => new PackageRepository(context));
            _permissionRepo = new Lazy<IPermissionRepository>(() => new PermissionRepository(context));
            _quoteStatusRepo = new Lazy<IQuoteStatusRepository>(() => new QuoteStatusRepository(context));
            _roleRepo = new Lazy<IRoleRepository>(() => new RoleRepository(context));
            _userPermissionRepo = new Lazy<IUserPermissionRepository>(() => new UserPermissionRepository(context));
            _userRepo = new Lazy<IUserRepository>(() => new UserRepository(context));
        }

        public IEventRepository Event => _eventRepo.Value;
        public ICompanyRepository Company => _companyRepo.Value;
        public ICompanyContactRepository CompanyContact => _companyContactRepo.Value;
        public IEquipmentRepository Equipment => _equipmentRepo.Value;
        public IEquipmentSetContentRepository EquipmentSetContent => _equipmentSetContentRepo.Value;
        public IEquipmentSetRepository EquipmentSet => _equipmentSetRepo.Value;
        public IEventEquipmentRepository EventEquipment => _eventEquipmentRepo.Value;
        public IEventGuestRepository EventGuest => _eventGuestRepo.Value;
        public IEventStaffRepository EventStaff => _eventStaffRepo.Value;
        public IGuestUserRepository GuestUser => _guestUserRepo.Value;
        public IPackageRepository Package => _packageRepo.Value;
        public IPermissionRepository Permission => _permissionRepo.Value;
        public IQuoteStatusRepository QuoteStatus => _quoteStatusRepo.Value;
        public IRoleRepository Role => _roleRepo.Value;
        public IUserPermissionRepository UserPermission => _userPermissionRepo.Value;
        public IUserRepository User => _userRepo.Value;

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}