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

            if (products == null || !products.Any())
            {
                return NoContent();
            }

            //  CHAMA A GERAÇÃO DE LINKS PARA CADA ITEM DA LISTA
            foreach (var produto in products)
            {
                GerarLinks(produto);
            }


            return Ok(products);
        }

        [HttpGet("{id}", Name = "GetProduct")]
        public async Task<ActionResult<ProductDTO>> Get(int id)
        {

            var product = await _productService.GetById(id);

            if (product == null)
            {
                // Retorna 404 Not Found com uma mensagem de erro
                return NotFound(new { message = $"Produto com id={id} não encontrado." });
            }

            GerarLinks(product);

            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ProductInputDTO productInputDto)
        {

            if (productInputDto == null)
            {
                return BadRequest("Dados do produto inválidos.");
            }

            await _productService.Add(productInputDto);

            return new CreatedAtRouteResult("GetProduct",
                new { id = productInputDto.Id }, productInputDto);
        }


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

                return NotFound();
            }

            await _productService.Remove(id);

            return NoContent();
        }



        private void GerarLinks(ProductDTO model)
        {
            if (model.Id > 0)
            {
                model.Links.Add(new LinkDto(
                    href: Url.Link("GetProduct", new { id = model.Id })!,
                    rel: "self",
                    metodo: "GET"
                ));


                model.Links.Add(new LinkDto(
                    href: Url.Link("GetProduct", new { id = model.Id })!,
                    rel: "update_product",
                    metodo: "PUT"
                ));


                model.Links.Add(new LinkDto(
                    href: Url.Link("GetProduct", new { id = model.Id })!,
                    rel: "delete_product",
                    metodo: "DELETE"
                ));
            }
        }

    }
}