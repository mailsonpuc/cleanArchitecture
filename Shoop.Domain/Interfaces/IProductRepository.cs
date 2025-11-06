
using Shoop.Domain.Entities;

namespace Shoop.Domain.Interfaces
{
    public interface IProductRepository
    {

        Task<IEnumerable<Product>> GetProductsAsync();
        Task<IEnumerable<Product>> GetProductsWithCategoryAsync(); //metodo especifico
        Task<Product> GetByIdAsync(int? id);
        Task<Product> CreateAsync(Product product);
        Task<Product> UpdateAsync(Product product);
        Task<Product> RemoveAsync(Product product);
    }
}