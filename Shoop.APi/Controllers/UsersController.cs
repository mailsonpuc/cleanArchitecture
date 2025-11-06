using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shoop.APi.Services;
using Shoop.Application.DTOs;
using Shoop.Application.Interfaces;

namespace Shoop.APi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UsersController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        /// <summary>
        /// Apenas admin [Authorize(Roles = "admin")]
        /// </summary>
        [HttpGet()]
        [Authorize(Roles = "admin")]
        [ResponseCache(VaryByHeader = "User-Agent", Location = ResponseCacheLocation.Any, Duration = 30)] //30 segundos
        public async Task<ActionResult<IEnumerable<UserDTO>>> Get()
        {
            var users = await _userService.GetUsers();

            if (users == null || !users.Any())
            {
                return NoContent();
            }

            //  CHAMA A GERAÇÃO DE LINKS PARA CADA ITEM DA LISTA
            // foreach (var categoria in categorias)
            // {
            //     GerarLinks(categoria);
            // }

            return Ok(users);
        }


        [HttpGet("{id}", Name = "GetUser")]
        public async Task<ActionResult<UserDTO>> Get(int id)
        {
            var product = await _userService.GetById(id);

            if (product == null)
            {
                return NotFound(new { message = $"User com id={id} não encontrado." });
            }

            // GerarLinks(categoria); // Mantenha ou remova, dependendo se você usa HATEOAS

            return Ok(product);
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Post([FromBody] UserDTO userDto)
        {
            if (userDto == null)
            {
                return BadRequest("Dados inválidos.");
            }

            UserDTO novo = new UserDTO()
            {
                Username = userDto.Username,
                Password = BCrypt.Net.BCrypt.HashPassword(userDto.Password),
                Role = userDto.Role
            };

            await _userService.Add(novo);

            return new CreatedAtRouteResult("GetUser",
                new { id = userDto.Id }, novo);
        }



     
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] UserDTO userDto)
        {
            if (id != userDto.Id)
            {
                return BadRequest("ID da rota não corresponde ao ID do corpo.");
            }

            if (userDto == null)
            {
                return BadRequest("Dados inválidos.");
            }

            // 1. Verifica se a senha foi enviada no DTO.
            if (!string.IsNullOrEmpty(userDto.Password))
            {
                //  APLICA HASH NO PUT, POIS É UMA NOVA SENHA 
                userDto.Password = BCrypt.Net.BCrypt.HashPassword(userDto.Password);
            }
            else
            {
                // 2. Se a senha não foi enviada (update parcial de Username/Role),
                // precisamos carregar o hash antigo do banco de dados para não sobrescrever
                // o hash existente no DB com um valor vazio/nulo.
                var existingUserDTO = await _userService.GetById(id);

                if (existingUserDTO != null)
                {
                    // 3. Preserva o hash antigo e atribui ao DTO que será enviado para o Update.
                    userDto.Password = existingUserDTO.Password;
                }
                // Se existingUserDTO for nulo, o Service.Update provavelmente falhará ou criará um problema,
                // mas essa verificação inicial (acima) lida com o NotFound.
            }

            // O Service agora recebe o DTO com a nova senha (hash) ou com o hash antigo preservado.
            await _userService.Update(userDto);

            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<UserDTO>> Delete(int id)
        {
            var user = await _userService.GetById(id);

            if (user == null)
            {
                return NotFound();
            }

            await _userService.Remove(id);

            return NoContent();
        }




        [HttpPost("login")]
        [AllowAnonymous] 
        public async Task<ActionResult<dynamic>> Authenticate([FromBody] UserDTO userLoginDto)
        {
            // 1. Buscar usuário pelo nome de usuário
            var userDto = await _userService.GetByUsername(userLoginDto.Username);

            if (userDto == null)
            {
                return NotFound(new { message = "Usuário ou senha inválidos" });
            }

            // VERIFICAÇÃO DA SENHA COM BCrypt.Verify
            // Compara a senha em texto puro do login com o hash armazenado (userDto.Password)
            if (!BCrypt.Net.BCrypt.Verify(userLoginDto.Password, userDto.Password))
            {
                // Se a senha não for válida (hash não confere)
                return NotFound(new { message = "Usuário ou senha inválidos" });
            }

            //  Gerar Token e Retornar
            var token = TokenService.GenerateToken(userDto);

            // Retorna os dados do usuário (sem a senha!) e o token
            // (Ajusta o UserDTO para garantir que a senha não seja serializada na resposta final)
            userDto.Password = null;

            return Ok(new
            {
                user = userDto,
                token = token
            });
        }

    }
}