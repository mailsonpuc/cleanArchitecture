

using Shoop.Application.DTOs;
using Shoop.Application.Interfaces;
using Shoop.Application.Mappings;
using Shoop.Domain.Interfaces;

namespace Shoop.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task Add(CategoryInputDTO categoryDTO)
        {
            var categoryEntity = categoryDTO.ToCategoryEntity();
            await _categoryRepository.CreateAsync(categoryEntity);
        }

        public async Task<CategoryDTO> GetById(int? id)
        {
            var categoryEntity = await _categoryRepository.GetByIdAsync(id);
            return categoryEntity is null ? null! : categoryEntity.ToCategoryDTO();
        }

        public async Task<IEnumerable<CategoryDTO>> GetCategories()
        {
            try
            {
                var categoriesEntity = await _categoryRepository.GetCategoriesAsync();
                return categoriesEntity.Select(category => category.ToCategoryDTO());
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public async Task Remove(int? id)
        {
            var categoryEntity = await _categoryRepository.GetByIdAsync(id);
            if (categoryEntity == null) return;
            await _categoryRepository.RemoveAsync(categoryEntity);
        }

        public async Task Update(CategoryDTO categoryDTO)
        {
            var categoryEntity = categoryDTO.ToCategoryEntity();
            await _categoryRepository.UpdateAsync(categoryEntity);
        }
    }
}