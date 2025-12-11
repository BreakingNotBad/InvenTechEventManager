using Contract.Interfaces.IRepository;
using Entity.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Repository.Infrastructure.Data;
using Repository.Repositories;

namespace Repository.Infrastructure.Repository
{
    public class CompanyContactRepository : RepositoryBase<CompanyContact>, ICompanyContactRepository
    {
        public CompanyContactRepository(RepositoryContext context) : base(context)
        {
        } 
        public async Task<IEnumerable<CompanyContact>> GetCompanyContactsAsync()
        {
            return await FindAll(trackChanges: false).ToListAsync();
        }
        public async Task<CompanyContact?> GetCompanyContactByIdAsync(int id)
        {
            return await FindByCondition(cc => cc.CompanyContactId == id, trackChanges: false)
                .FirstOrDefaultAsync();
        }
    }
}
