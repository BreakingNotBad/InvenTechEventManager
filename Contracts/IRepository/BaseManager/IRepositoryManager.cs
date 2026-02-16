namespace Contracts.IRepository.BaseManager
{
    public interface IRepositoryManager
    {
        IEventRepository Event { get; }
        ICompanyRepository Company { get; }
        ICompanyContactRepository CompanyContact { get; }
        IStaffRepository Staff { get; }
        IOutsourceRepository Outsource { get; }
        IEquipmentRepository Equipment { get; }
        IPackageRepository Package { get; }
        IRoleRepository Role { get; }
        ICategoryRepository Category { get; }
        IRefreshTokenRepository RefreshToken { get; }
        Task SaveAsync();
    }
}
