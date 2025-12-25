using Entity.Domain.Model;


namespace Contract.Interfaces.IRepository
{
    public interface ICompanyRepository : IRepositoryBase<Company>
    {
        Task<IEnumerable<Company>> GetCompaniesAsync();
        Task<Company?> GetCompanyByIdAsync(int id);
        void CreateCompany(Company company);
        void DeleteCompany(Company company);
    }
}
