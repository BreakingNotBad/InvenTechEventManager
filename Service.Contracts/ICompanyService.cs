using Contracts.DTOs;
using Entities.Models;

namespace Service.Contracts
{
    public interface ICompanyService
    {
        Task<IEnumerable<Company>> GetCompaniesAsync(
            string? companyName,
            string? companyContact,
            string? Address,
            decimal? Latitude,
            decimal? Longitude,
            bool? IsDeleted,
            DateTime CreatedAt,
            DateTime UpdatedAt);
        Task<Company?> GetCompanyByIdAsync(int id);
        Task<Company> CreateCompanyAsync(CreateCompanyDto dto);
        Task<Company> UpdateCompanyAsync(int id, UpdateCompanyDto dto);
        Task DeleteCompanyAsync(int id);
    }
}
