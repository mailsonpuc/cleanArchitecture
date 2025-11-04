

using Microsoft.EntityFrameworkCore;
using Shoop.Domain.Entities;
using Shoop.Domain.Interfaces;
using Shoop.Infrastructure.Context;

namespace Shoop.Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private ApplicationDbContext _categoryContext;

        public CategoryRepository(ApplicationDbContext categoryContext)
        {
            _categoryContext = categoryContext;
        }

        public async Task<Category> CreateAsync(Category category)
        {
            _categoryContext.Add(category);
            await _categoryContext.SaveChangesAsync();
            return category;
        }

        public async Task<Category> GetByIdAsync(int? id)
        {

#pragma warning disable CS8603 // Possible null reference return.
            return await _categoryContext.Categories.FindAsync(id);
#pragma warning restore CS8603 // Possible null reference return.
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            try
            {
                var categorias = await _categoryContext.Categories.ToListAsync();
                return categorias;
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public async Task<Category> RemoveAsync(Category category)
        {
            _categoryContext.Remove(category);
            await _categoryContext.SaveChangesAsync();
            return category;
        }

        public async Task<Category> UpdateAsync(Category category)
        {
            _categoryContext.Update(category);
            await _categoryContext.SaveChangesAsync();
            return category;
        }
    }
}