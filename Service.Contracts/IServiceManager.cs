namespace Service.Contract
{
    public interface IServiceManager
    {
        IEventService Event { get; }
        ICompanyService Company { get; }
        ICompanyContactService CompanyContact { get; }
        IEquipmentService Equipment { get; }
        IEquipmentSetContentService EquipmentSetContent { get; }
        IEquipmentSetService EquipmentSet { get; }
        IEventEquipmentService EventEquipment { get; }
        IEventGuestService EventGuest { get; }
        IEventStaffService EventStaff { get; }
        IGuestUserService GuestUser { get; }
        IPackageService Package { get; }
        IPermissionService Permission { get; }
        IQuoteStatusService QuoteStatus { get; }
        IRoleService Role { get; }
        IUserPermissionService UserPermission { get; }
        IUserService User { get; }
    }
}
