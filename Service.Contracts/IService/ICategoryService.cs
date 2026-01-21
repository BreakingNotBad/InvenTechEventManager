using Entities.Models;

namespace Service.Contracts.IService
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetCategoryByAsync();
    }
}
