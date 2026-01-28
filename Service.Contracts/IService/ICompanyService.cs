using Service.Contracts.DTOs.Company;
using Shared.RequestFeatures.Parameters;

namespace Service.Contracts.IService
{
    public interface ICompanyService
    {
        Task<IEnumerable<CompanyDto>> GetCompaniesAsync(
            CompanyParameter parameters
        );
        Task<CompanyDto?> GetCompanyByIdAsync(int id);
        Task<CompanyDto> CreateCompanyAsync(CreateCompanyDto companyDto);
        Task<CompanyDto> UpdateCompanyAsync(int id, UpdateCompanyDto companyDto);
        Task DeleteCompanyAsync(int id);
    }
}
