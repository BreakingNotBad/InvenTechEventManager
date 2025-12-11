using Contract.Interfaces.IRepository;
using Entity.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Repository.Infrastructure.Data;
using Repository.Repositories;

namespace Repository.Infrastructure.Repository
{
    public class QuoteStatusRepository : RepositoryBase<QuoteStatus>, IQuoteStatusRepository
    {
        public QuoteStatusRepository(RepositoryContext context) : base(context)
        {
        }

        public async Task<IEnumerable<QuoteStatus>> GetQuoteStatusesAsync()
        {
            return await FindAll(trackChanges: false).ToListAsync();
        }

        public async Task<QuoteStatus?> GetQuoteStatusByIdAsync(int id)
        {
            return await FindByCondition(e => e.QuoteStatusId == id, trackChanges: false)
                .FirstOrDefaultAsync();
        }
    }
}
