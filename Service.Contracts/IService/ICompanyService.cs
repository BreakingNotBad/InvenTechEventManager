using Service.Contracts.DTOs.Company;

namespace Service.Contracts.IService
{
    public interface ICompanyService
    {
        Task<IEnumerable<CompanyDto>> GetCompaniesAsync(
            string? companyName,
            string? companyContact,
            string? Address,
            decimal? Latitude,
            decimal? Longitude,
            bool? IsDeleted,
            DateTime CreatedAt,
            DateTime UpdatedAt
        );
        Task<CompanyDto?> GetCompanyByIdAsync(int id);
        Task<CompanyDto> CreateCompanyAsync(CreateCompanyDto companyDto);
        Task<CompanyDto> UpdateCompanyAsync(int id, UpdateCompanyDto companyDto);
        Task DeleteCompanyAsync(int id);
    }
}
