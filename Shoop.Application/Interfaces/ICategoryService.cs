

using Shoop.Application.DTOs;

namespace Shoop.Application.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDTO>> GetCategories();
        Task<CategoryDTO> GetById(int? id);
        Task Add(CategoryInputDTO categoryDTO); //CategoryInputDTO limpo sem hateos
        Task Update(CategoryDTO categoryDTO);
        Task Remove(int? id);
    }
}