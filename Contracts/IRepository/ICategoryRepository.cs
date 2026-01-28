using Entities.Models;
using Shared.RequestFeatures.Parameters;

namespace Contracts.IRepository
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllCategoryAsync(CategoryParameter categoryParameter, bool trackChanges);
    }
}
