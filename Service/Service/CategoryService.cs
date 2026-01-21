using Contracts.IRepository.BaseManager;
using Entities.Models;
using Service.Contracts.IService;

namespace Service.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepositoryManager _repo;

        public CategoryService(IRepositoryManager repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<Category>> GetCategoryByAsync()
        {
            return await _repo.Category.GetAllCategoryAsync();
        }
    }
}
