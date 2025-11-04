

using System.ComponentModel.DataAnnotations;

namespace Shoop.Application.DTOs
{
    public class UserDTO
    {
        public int Id { get; set; }


        [Required(ErrorMessage = "Informe o nome do usuario")]
        [MinLength(3, ErrorMessage = "Minino é 3")]
        [MaxLength(100, ErrorMessage = "Maximo é 100")]
        public string Username { get; set; } = string.Empty;



        [Required(ErrorMessage = "Informe a senha de usuario")]
        [MinLength(3, ErrorMessage = "Minino é 3")]
        [MaxLength(100, ErrorMessage = "Maximo é 100")]
        public string Password { get; set; } = string.Empty;

      
        [Required(ErrorMessage = "Informe a regra do usuario")]
        [MinLength(3, ErrorMessage = "Minino é 5")]
        [MaxLength(100, ErrorMessage = "Maximo é 100")]
        public string Role { get;  set; } = string.Empty;

    }
}