using Contract.Interfaces.IRepository;
using Entity.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Repository.Infrastructure.Data;
using Repository.Repositories;

namespace Repository.Infrastructure.Repository
{
    public class CompanyRepository : RepositoryBase<Companies>, ICompanyRepository
    {
        public CompanyRepository(RepositoryContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Companies>> GetCompaniesAsync()
        {
            return await FindAll(trackChanges: false).ToListAsync();
        }

        public async Task<Companies?> GetCompanyByIdAsync(int id)
        {
            return await FindByCondition(e => e.CompanyId == id, trackChanges: false)
                .FirstOrDefaultAsync();
        }
        public void CreateCompany(Companies company)
        {
            Create(company);
        }

        public void DeleteCompany(Companies company)
        {
            Delete(company);
        }
        public void UpdateCompany(Companies company)
        {
            Update(company);
        }
    }
}
