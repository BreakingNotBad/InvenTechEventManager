using Contract.DTOs.Company;
using Entity.Domain.Model;

namespace Service.Contract
{
    public interface ICompanyService
    {
        Task<IEnumerable<Company>> GetCompaniesAsync(
            string? companyName,
            string? companyContact);
        Task<Company?> GetCompanyByIdAsync(int id);
        Task <Company>CreateCompanyAsync(CreateCompanyDto dto);
        Task DeleteCompanyAsync(int id);
        Task UpdateCompanyAsync(int id, UpdateCompanyDto dto);
        Task SoftDeleteCompanyAsync(int id, bool isDeleted);
        Task<Company?> GetCompanyContactByCompanyIdAsync (int id);
    }
}
