using Entities.Models;

namespace Service.Contracts
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetCategoryByAsync();
    }
}
