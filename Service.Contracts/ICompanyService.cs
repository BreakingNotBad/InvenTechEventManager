using Entity.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contract
{
    public interface ICompanyService
    {
        Task<IEnumerable<Companies>> GetCompaniesAsync();
        Task<Companies?> GetCompanyByIdAsync(int id);
        Task CreateCompanyAsync(Companies company);
        Task DeleteCompanyAsync(int id);
        Task UpdateCompanyAsync(Companies company);

        Task<Companies?> GetCompanyContactByCompanyIdAsync (int id);
    }
}
