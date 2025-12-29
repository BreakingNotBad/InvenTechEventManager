using Entity.Domain.Model;

namespace Service.Contract
{
    public interface ICompanyService
    {
        Task<IEnumerable<Company>> GetCompaniesAsync();
        Task<Company?> GetCompanyByIdAsync(int id);
        Task CreateCompanyAsync(Company company);
        Task DeleteCompanyAsync(int id);
        Task UpdateCompanyAsync(int id, Company company);

        Task<Company?> GetCompanyContactByCompanyIdAsync (int id);
    }
}
