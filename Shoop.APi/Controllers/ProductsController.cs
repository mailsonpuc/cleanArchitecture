using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Shoop.Application.DTOs;
using Shoop.Application.Interfaces;

namespace Shoop.APi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductsController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        [HttpGet]
        [ResponseCache(VaryByHeader = "User-Agent", Location = ResponseCacheLocation.Any, Duration = 30)] //30 segundos
        // O tipo de retorno agora é Task<ActionResult<IEnumerable<ProductDTO>>>
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProducts()
        {
            var products = await _productService.GetProducts();

            // Boa prática: Retornar 204 No Content se a lista estiver vazia.
            if (products == null || !products.Any())
            {
                return NoContent(); // Status 204
            }

            // Retorna a lista de DTOs com Status 200 OK
            return Ok(products); 
        }

        [HttpGet("{id}", Name = "GetProduct")]
        public async Task<ActionResult<ProductDTO>> Get(int id)
        {
            // O Service deve chamar um método que inclua a Category (GetByIdAsync no Repositório)
            var product = await _productService.GetById(id);

            if (product == null)
            {
                // Retorna 404 Not Found com uma mensagem de erro
                return NotFound(new { message = $"Produto com id={id} não encontrado." });
            }

            // GerarLinks(categoria); // Mantenha ou remova, dependendo se você usa HATEOAS

            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ProductDTO productDto)
        {
            // Validação de entrada
            if (productDto == null)
            {
                return BadRequest("Dados do produto inválidos.");
            }
            
            // Note que o Service irá criar o produto e preencher o Id
            await _productService.Add(productDto);

            // Retorna 201 CreatedAtRoute com a URI do novo recurso
            return new CreatedAtRouteResult("GetProduct",
                new { id = productDto.Id }, productDto);
        }
        
        // Exemplo de Put (Atualização)
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] ProductDTO productDto)
        {
            if (id != productDto.Id)
            {
                return BadRequest("ID da rota não corresponde ao ID do corpo.");
            }

            if (productDto == null)
            {
                return BadRequest("Dados do produto inválidos.");
            }

            await _productService.Update(productDto);

            // Retorna 204 No Content após uma atualização bem-sucedida (boa prática REST)
            return NoContent();
        }

        // Exemplo de Delete
        [HttpDelete("{id}")]
        public async Task<ActionResult<ProductDTO>> Delete(int id)
        {
            var product = await _productService.GetById(id);

            if (product == null)
            {
                // Se o produto não existir, retorna 404
                return NotFound();
            }

            await _productService.Remove(id);

            // Retorna 204 No Content após uma exclusão bem-sucedida
            return NoContent(); 
        }
    }
}