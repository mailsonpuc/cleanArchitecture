
using Shoop.Application.DTOs;


namespace Shoop.Application.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDTO>> GetProducts();
        Task<ProductDTO> GetById(int? id);
        // Task Add(ProductDTO productDTO);
        Task Add(ProductInputDTO productDTO); //ProductInputDTOProductInputDTO limpo sem hateos
        Task Update(ProductDTO productDTO);
        Task Remove(int? id);
    }
}