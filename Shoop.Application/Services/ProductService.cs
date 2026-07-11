
using Shoop.Application.DTOs;
using Shoop.Application.Interfaces;
using Shoop.Application.Mappings;
using Shoop.Domain.Interfaces;

namespace Shoop.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ProductDTO> GetById(int? id)
        {
            var productEntity = await _productRepository.GetByIdAsync(id);
            return productEntity is null ? null! : productEntity.ToProductDTO();
        }

        public async Task<IEnumerable<ProductDTO>> GetProducts()
        {
            try
            {
                var productEntity = await _productRepository.GetProductsWithCategoryAsync();
                return productEntity.Select(product => product.ToProductDTO());
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public async Task Remove(int? id)
        {
            var productEntity = await _productRepository.GetByIdAsync(id);
            if (productEntity == null) return;
            await _productRepository.RemoveAsync(productEntity);
        }

        public async Task Update(ProductDTO productDTO)
        {
            var productEntity = productDTO.ToProductEntity();
            await _productRepository.UpdateAsync(productEntity);
        }

        public async Task Add(ProductInputDTO productDTO)
        {
            var productEntity = productDTO.ToProductEntity();
            await _productRepository.CreateAsync(productEntity);
        }
    }
}