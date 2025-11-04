

using Microsoft.EntityFrameworkCore;
using Shoop.Domain.Entities;
using Shoop.Domain.Interfaces;
using Shoop.Infrastructure.Context;

namespace Shoop.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private ApplicationDbContext _productContext;

        public ProductRepository(ApplicationDbContext productContext)
        {
            _productContext = productContext;
        }

        public async Task<Product> CreateAsync(Product product)
        {
            _productContext.Add(product);
            await _productContext.SaveChangesAsync();
            return product;
        }

        public async Task<Product> GetByIdAsync(int? id)
        {

#pragma warning disable CS8603 // Possible null reference return.
            return await _productContext.Products.FindAsync(id);
#pragma warning restore CS8603 // Possible null reference return.
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            try
            {
                var products = await _productContext.Products.ToListAsync();
                return products;
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public async Task<Product> RemoveAsync(Product product)
        {
            _productContext.Remove(product);
            await _productContext.SaveChangesAsync();
            return product;
        }

        public async Task<Product> UpdateAsync(Product product)
        {
            _productContext.Update(product);
            await _productContext.SaveChangesAsync();
            return product;
        }
    }
}