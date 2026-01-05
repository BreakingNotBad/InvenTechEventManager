using Contract.Interfaces.IRepository;
using Entity.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Repository.Infrastructure.Data;
using Repository.Infrastructure.Repository.BaseManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Infrastructure.Repository
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
