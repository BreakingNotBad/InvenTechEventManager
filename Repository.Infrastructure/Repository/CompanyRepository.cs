using Contract.Interfaces.IRepository;
using Entity.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Repository.Infrastructure.Data;
using Repository.Repositories;

namespace Repository.Infrastructure.Repository
{
    public class CompanyRepository : RepositoryBase<Company>, ICompanyRepository
    {
        public CompanyRepository(RepositoryContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Company>> GetCompaniesAsync()
        {
            return await FindAll(trackChanges: false).ToListAsync();
        }

        public async Task<Company?> GetCompanyByIdAsync(int id)
        {
            return await FindByCondition(e => e.CompanyId == id, trackChanges: false)
                .FirstOrDefaultAsync();
        }
    }
}
