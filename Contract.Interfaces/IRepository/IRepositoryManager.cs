namespace Contract.Interfaces.IRepository
{
    public interface IRepositoryManager
    {
        IEventRepository Event { get; }
        ICompanyRepository Company { get; }
        ICompanyContactRepository CompanyContact { get; }
        IEquipmentRepository Equipment { get; }
        IEquipmentSetContentRepository EquipmentSetContent { get; }
        IEquipmentSetRepository EquipmentSet { get; }
        IEventEquipmentRepository EventEquipment { get; }
        IEventGuestRepository EventGuest { get; }
        IEventStaffRepository EventStaff { get; }
        IGuestUserRepository GuestUser { get; }
        IPackageRepository Package { get; }
        IPermissionRepository Permission { get; }
        IQuoteStatusRepository QuoteStatus { get; }
        IRoleRepository Role { get; }
        IUserPermissionRepository UserPermission { get; }
        IUserRepository User { get; }
        Task SaveAsync();
    }
}
