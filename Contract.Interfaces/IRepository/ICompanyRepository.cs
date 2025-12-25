using Entity.Domain.Model;


namespace Contract.Interfaces.IRepository
{
    public interface ICompanyRepository : IRepositoryBase<Company>
    {
        Task<IEnumerable<Company>> GetCompaniesAsync();
        Task<Company?> GetCompanyByIdAsync(int id);
        Task DeleteCompany(Company company);
        Task UpdateCompany(Company company);
    }
}
