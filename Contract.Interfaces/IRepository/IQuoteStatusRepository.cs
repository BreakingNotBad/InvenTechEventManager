using Entity.Domain.Model;

namespace Contract.Interfaces.IRepository
{
    public interface IQuoteStatusRepository : IRepositoryBase<QuoteStatus>
    {
        Task<IEnumerable<QuoteStatus>> GetQuoteStatusesAsync();
        Task<QuoteStatus?> GetQuoteStatusByIdAsync(int id);
    }
}
