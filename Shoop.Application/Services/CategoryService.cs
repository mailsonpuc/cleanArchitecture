

using AutoMapper;
using Shoop.Application.DTOs;
using Shoop.Application.Interfaces;
using Shoop.Domain.Entities;
using Shoop.Domain.Interfaces;

namespace Shoop.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }



        public async Task Add(CategoryDTO categoryDTO)
        {

            var categoryEntity = _mapper.Map<Category>(categoryDTO);
            await _categoryRepository.CreateAsync(categoryEntity);

        }



        public async Task<CategoryDTO> GetById(int? id)
        {
            var categoryEntity = await _categoryRepository.GetByIdAsync(id);
            return _mapper.Map<CategoryDTO>(categoryEntity);
        }

        public async Task<IEnumerable<CategoryDTO>> GetCategories()
        {
            try
            {
                var categoriesEntity = await _categoryRepository.GetCategoriesAsync();
                var categoriasDto = _mapper.Map<IEnumerable<CategoryDTO>>(categoriesEntity);
                return categoriasDto;
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
            var categoryEntity = _mapper.Map<Category>(categoryDTO);
            await _categoryRepository.UpdateAsync(categoryEntity);
        }
    }
}