using Entity.Domain.Model;


namespace Contract.Interfaces.IRepository
{
    public interface ICompanyRepository : IRepositoryBase<Companies>
    {
        Task<IEnumerable<Companies>> GetCompaniesAsync();
        Task<Companies?> GetCompanyByIdAsync(int id);
        void CreateCompany(Companies company);
        void DeleteCompany(Companies company);
        void UpdateCompany(Companies company);
    }
}
