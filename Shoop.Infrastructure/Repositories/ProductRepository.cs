using Microsoft.EntityFrameworkCore;
using Shoop.Domain.Entities;
using Shoop.Domain.Interfaces;
using Shoop.Infrastructure.Context;


namespace Shoop.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _productContext;

        public ProductRepository(ApplicationDbContext productContext)
        {
            _productContext = productContext;
        }


        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            // Este método não inclui a Category. Pode ser usado por serviços que não precisam do detalhe da categoria.
            return await _productContext.Products.AsNoTracking().ToListAsync();
        }


        public async Task<IEnumerable<Product>> GetProductsWithCategoryAsync()
        {
            // Usa .Include() para carregar a entidade Category junto com o Product.
            return await _productContext.Products
                                        .Include(p => p.Category)
                                        .AsNoTracking()
                                        .ToListAsync();
        }


        // --- Métodos de CRUD ---

        public async Task<Product> CreateAsync(Product product)
        {
            _productContext.Add(product);
            await _productContext.SaveChangesAsync();
            return product;
        }

        public async Task<Product> GetByIdAsync(int? id)
        {
            #pragma warning disable CS8603
            return await _productContext.Products
                .Include(p => p.Category)
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id); 
            #pragma warning restore CS8603
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