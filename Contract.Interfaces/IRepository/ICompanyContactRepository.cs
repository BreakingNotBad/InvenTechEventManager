using Entity.Domain.Model;


namespace Contract.Interfaces.IRepository
{
    public interface ICompanyContactRepository : IRepositoryBase<CompanyContact>
    {
        Task<IEnumerable<CompanyContact>> GetCompanyContactsAsync();
        Task<CompanyContact?> GetCompanyContactByIdAsync(int id);
    }
}
