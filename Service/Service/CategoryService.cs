using AutoMapper;
using Contracts.IRepository.BaseManager;
using Entities.Models;
using Service.Contracts.DTOs.Category;
using Service.Contracts.IService;
using Shared.RequestFeatures.Parameters;

namespace Service.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepositoryManager _repo;
        private readonly IMapper _mapper;

        public CategoryService(IRepositoryManager repo , IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CategoryDto>> GetCategoryByAsync(CategoryParameter categoryParameter)
        {
            // ดึงข้อมูล Entity
            var category = await _repo.Category.GetAllCategoryAsync(categoryParameter, false);

            // 4. แปลงจาก Entity เป็น DTO
            var categoryDto = _mapper.Map<IEnumerable<CategoryDto>>(category);

            return categoryDto;
        }
    }
}
