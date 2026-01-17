using Contracts.DTOs;
using Entities.Models;

namespace Service.Contracts
{
    public interface ICompanyService
    {
        Task<IEnumerable<Company>> GetCompaniesAsync(string? companyName, string? companyContact);
        Task<Company?> GetCompanyByIdAsync(int id);
        Task<Company> CreateCompanyAsync(CreateCompanyDto dto);
        Task<Company> UpdateCompanyAsync(int id, UpdateCompanyDto dto);
        Task DeleteCompanyAsync(int id);
    }
}
