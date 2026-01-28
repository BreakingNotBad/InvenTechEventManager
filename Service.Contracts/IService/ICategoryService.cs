using Entities.Models;
using Service.Contracts.DTOs.Category;
using Shared.RequestFeatures.Parameters;

namespace Service.Contracts.IService
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> GetCategoryByAsync(CategoryParameter categoryParameter);
    }
}
