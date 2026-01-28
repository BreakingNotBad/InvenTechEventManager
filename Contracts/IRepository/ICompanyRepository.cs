using Contracts.IRepository.BaseManager;
using Entities.Models;
using Shared.RequestFeatures.Parameters;

namespace Contracts.IRepository
{
    public interface ICompanyRepository : IRepositoryBase<Company>
    {
        Task<IEnumerable<Company>> GetCompaniesAsync(CompanyParameter parameters, bool trackChanges);
        Task<Company?> GetCompanyByIdAsync(int id,bool trackchanges);
        void CreateCompany(Company company);
        void DeleteCompany(Company company);
        void UpdateCompany(Company company);
    }
}
