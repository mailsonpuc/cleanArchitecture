
using AutoMapper;
using Shoop.Application.DTOs;
using Shoop.Application.Interfaces;
using Shoop.Domain.Entities;
using Shoop.Domain.Interfaces;

namespace Shoop.Application.Services
{
    public class ProductService : IProductService
    {
        private IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task Add(ProductDTO productDTO)
        {
            var productEntity = _mapper.Map<Product>(productDTO);
            await _productRepository.CreateAsync(productEntity);

        }

        public async Task<ProductDTO> GetById(int? id)
        {
            var productEntity = await _productRepository.GetByIdAsync(id);
            //  GetByIdAsync no Repositório agora tem o .Include())
            return _mapper.Map<ProductDTO>(productEntity);

        }

        public async Task<IEnumerable<ProductDTO>> GetProducts()
        {
            try
            {
                var productEntity = await _productRepository.GetProductsWithCategoryAsync();
                var productDto = _mapper.Map<IEnumerable<ProductDTO>>(productEntity);
                return productDto;
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
            var productEntity = _mapper.Map<Product>(productDTO);
            await _productRepository.UpdateAsync(productEntity);

        }
    }
}