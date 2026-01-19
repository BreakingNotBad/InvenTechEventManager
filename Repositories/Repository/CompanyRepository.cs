using Contracts.IRepository;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Data;
using Repositories.Repository.BaseManager;

namespace Repositories.Repository
{
    public class CompanyRepository : RepositoryBase<Company>, ICompanyRepository
    {
        public CompanyRepository(RepositoryContext context)
            : base(context) { }

        public async Task<IEnumerable<Company>> GetCompaniesAsync()
        {
            return await FindAll(trackChanges: false).Include(e => e.CompanyContacts).ToListAsync();
        }

        public async Task<Company?> GetCompanyByIdAsync(int id, bool trackchanges)
        {
            return await FindByCondition(e => e.CompanyId == id, trackchanges)
                .Include(e => e.CompanyContacts)
                .FirstOrDefaultAsync();
        }

        public void CreateCompany(Company company)
        {
            Create(company);
        }

        public void UpdateCompany(Company company)
        {
            Update(company);
        }

        public void DeleteCompany(Company company)
        {
            Delete(company);
        }
    }
}
