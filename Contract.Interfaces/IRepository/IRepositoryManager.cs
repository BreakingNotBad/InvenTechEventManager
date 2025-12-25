namespace Contract.Interfaces.IRepository
{
    public interface IRepositoryManager
    {
        IEventRepository Event { get; }
        ICompanyRepository Company { get; }
        IStaffRepository Staff { get; }
        IOutsourceRepository Outsource { get; }
        IEquipmentRepository Equipment { get; }
        IPackageRepository Package { get; }
        Task SaveAsync();
    }
}
