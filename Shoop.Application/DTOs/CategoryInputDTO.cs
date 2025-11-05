using System.ComponentModel.DataAnnotations;


namespace Shoop.Application.DTOs
{
    public class CategoryInputDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Informe o nome da categoria")]
        [MinLength(3,   ErrorMessage = "Minino é 3")]
        [MaxLength(100, ErrorMessage = "Maximo é 100")]
        public string Title { get;  set; } = string.Empty;
    }
}