using Contracts.IRepository;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Data;
using Repositories.Repository.BaseManager;

namespace Repositories.Repository
{
    public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
    {
        public CategoryRepository(RepositoryContext context)
            : base(context) { }

        public async Task<IEnumerable<Category>> GetAllCategoryAsync()
        {
            return await FindAll(trackChanges: false).ToListAsync();
        }
    }
}
