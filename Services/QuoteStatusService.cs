using Contract.Interfaces.IRepository;
using Entity.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Service.Contract;

namespace Service
{
    public class QuoteStatusService : IQuoteStatusService
    {
        private readonly IRepositoryManager _repo;

        public QuoteStatusService(IRepositoryManager repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<QuoteStatus>> GetQuoteStatusesAsync()
        {
            return await _repo.QuoteStatus.GetQuoteStatusesAsync();
        }

        public async Task<QuoteStatus?> GetQuoteStatusByIdAsync(int id)
        {
            var query = _repo.QuoteStatus.FindByCondition(
                e => e.QuoteStatusId == id,
                trackChanges: false
            );

            return await query.FirstOrDefaultAsync();
        }
    }
}
