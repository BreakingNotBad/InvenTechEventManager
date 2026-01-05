using Contract.Interfaces.IRepository.BaseManager;
using Entity.Domain.Model;
using Service.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class CategoryService: ICategoryService
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
