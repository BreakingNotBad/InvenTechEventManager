using Entity.Domain.Model;

namespace Service.Contract
{
    public interface ICompanyContactService
    {
        Task<IEnumerable<CompanyContact>> GetCompanyContactsAsync();
        Task<CompanyContact?> GetCompanyContactByIdAsync(int id);
    }
}
