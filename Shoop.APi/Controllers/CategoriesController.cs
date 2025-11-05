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

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }



        [HttpGet()]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> Get()
        {
            try
            {
                var categorias = await _categoryService.GetCategories();
                return Ok(categorias);
            }
            catch
            {
                throw;
            }
        }



        [HttpGet("{id}", Name = "GetCategoria")]
        public async Task<ActionResult<Category>> Get(int id)
        {
            var categoria = await _categoryService.GetById(id);

            if (categoria == null)
            {
                return NotFound(new { message = $"Categoria com id={id} não encontrado." });
            }

            return Ok(categoria);
        }



        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CategoryDTO categoriaDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _categoryService.Add(categoriaDto);

            return new CreatedAtRouteResult("GetCategoria",
                new { id = categoriaDto.Id }, categoriaDto);
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


    }

}