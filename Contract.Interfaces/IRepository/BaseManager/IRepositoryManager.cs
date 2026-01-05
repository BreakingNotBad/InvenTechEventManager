namespace Contract.Interfaces.IRepository.BaseManager
{
    public interface IRepositoryManager
    {
        IEventRepository Event { get; }
        ICompanyRepository Company { get; }
        IStaffRepository Staff { get; }
        IOutsourceRepository Outsource { get; }
        IEquipmentRepository Equipment { get; }
        IPackageRepository Package { get; }
        IRoleRepository Role { get; }
        ICategoryRepository Category { get; }
        Task SaveAsync();
    }
}
