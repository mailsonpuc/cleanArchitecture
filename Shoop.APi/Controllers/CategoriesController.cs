using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Shoop.Application.DTOs;
using Shoop.Application.Interfaces;
using Shoop.Domain.Entities;


namespace Shoop.APi.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper; //  Injete o IMapper

        public CategoriesController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }



        /// <summary>
        /// GetAll Category com cache de 1 hora + geração de links.
        /// </summary>
        [HttpGet()]
        [ResponseCache(VaryByHeader = "User-Agent", Location = ResponseCacheLocation.Any, Duration = 3600)] //1 hora
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> Get()
        {
            var categorias = await _categoryService.GetCategories();

            if (categorias == null || !categorias.Any())
            {
                return NoContent(); // Resposta HTTP 204 se não houver conteúdo
            }

            //  CHAMA A GERAÇÃO DE LINKS PARA CADA ITEM DA LISTA
            foreach (var categoria in categorias)
            {
                GerarLinks(categoria);
            }

            return Ok(categorias);
        }




        [HttpGet("{id}", Name = "GetCategoria")] // Nome da rota para usar no HATEOAS
        public async Task<ActionResult<CategoryDTO>> Get(int id)
        {
            var categoria = await _categoryService.GetById(id);

            if (categoria == null)
            {
                return NotFound(new { message = $"Categoria com id={id} não encontrado." });
            }

            //  CHAMA A GERAÇÃO DE LINKS PARA O RECURSO ÚNICO
            GerarLinks(categoria);

            return Ok(categoria);
        }



        // [HttpPost]
        // public async Task<ActionResult> Post([FromBody] CategoryInputDTO categoriaDto)
        // {
        //     if (!ModelState.IsValid)
        //     {
        //         return BadRequest(ModelState);
        //     }

        //     var categoriaDto = _mapper.Map<CategoryDTO>(categoriaInputDto);

        //     await _categoryService.Add(categoriaDto);

        //     return new CreatedAtRouteResult("GetCategoria",
        //         new { id = categoriaDto.Id }, categoriaDto);
        // }


        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CategoryInputDTO categoriaInputDto)
        {

            await _categoryService.Add(categoriaInputDto);


            return new CreatedAtRouteResult("GetCategoria",
                new { id = categoriaInputDto.Id }, categoriaInputDto);
        }



        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] CategoryDTO categoriaDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != categoriaDto.Id)
            {
                return NotFound(new { message = "Categoria não encontrada." });
            }

            await _categoryService.Update(categoriaDto);
            return Ok(categoriaDto);
        }




        /// <summary>
        /// Deletes a specific Category.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<Category>> Delete(int id)
        {
            try
            {
                var categoriaDto = await _categoryService.GetById(id);

                if (categoriaDto == null)
                {
                    return NotFound(new { message = "Categoria não encontrada" });
                }

                await _categoryService.Remove(id);

                return Ok(new { message = "Categoria removida com sucesso." });
            }

            catch (System.Exception)
            {
                return BadRequest(new { message = "Não foi possivel remove a categoria" });
            }

        }




        private void GerarLinks(CategoryDTO model)
        {
            if (model.Id > 0)
            {
                // Link SELF (GET) - Usa o nome da rota "GetCategoria"
                model.Links.Add(new LinkDto(
                    href: Url.Link("GetCategoria", new { id = model.Id })!,
                    rel: "self",
                    metodo: "GET"
                ));

                // Link de ATUALIZAÇÃO (PUT)
                model.Links.Add(new LinkDto(
                    href: Url.Link("GetCategoria", new { id = model.Id })!,
                    rel: "update_category",
                    metodo: "PUT"
                ));

                // Link de DELEÇÃO (DELETE)
                model.Links.Add(new LinkDto(
                    href: Url.Link("GetCategoria", new { id = model.Id })!,
                    rel: "delete_category",
                    metodo: "DELETE"
                ));
            }
        }


    }

}