using Entity.Domain.Model;

namespace Service.Contract
{
    public interface IQuoteStatusService
    {
        Task<IEnumerable<QuoteStatus>> GetQuoteStatusesAsync();
        Task<QuoteStatus?> GetQuoteStatusByIdAsync(int id);
    }
}
