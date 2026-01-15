namespace Service.Contract.Manager
{
    public interface IServiceManager
    {
        IEventService Event { get; }
        ICompanyService Company { get; }
        IStaffService Staff { get; }
        IOutsourceService Outsource { get; }
        IEquipmentService Equipment { get; }
        IPackageService Package { get; }
        IRoleService Role { get; }
        ICategoryService Category { get; }
        IFileService File { get; }
    }
}
