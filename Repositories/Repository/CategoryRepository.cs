using Contracts.IRepository;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Data;
using Repositories.Repository.BaseManager;
using Shared.RequestFeatures.Parameters;

namespace Repositories.Repository
{
    public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
    {
        public CategoryRepository(RepositoryContext context)
            : base(context) { }

        public async Task<IEnumerable<Category>> GetAllCategoryAsync(CategoryParameter categoryParameter, bool trackChanges)
        {
            // 1. เริ่มต้น Query
            var items = FindAll(trackChanges);

            // 2. Filter: Search by RoleName
            if (!string.IsNullOrWhiteSpace(categoryParameter.CategoryName))
            {
                items = items.Where(o => o.CategoryName.ToLower().Contains(categoryParameter.CategoryName.ToLower()));
            }
            return await items.ToListAsync();
        }
    }
}
