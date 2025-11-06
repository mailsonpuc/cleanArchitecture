

using System.ComponentModel.DataAnnotations;
using Shoop.Domain.Entities;

namespace Shoop.Application.DTOs
{
    public class ProductInputDTO
    {
     
        public int Id { get; set; }


        [Required(ErrorMessage = "Informe o titulo")]
        [MinLength(3, ErrorMessage = "Minino é 3")]
        [MaxLength(100, ErrorMessage = "Maximo é 100")]
        public string Title { get;  set; } = string.Empty;



        [Required(ErrorMessage = "Informe a descrição")]
        [MinLength(3, ErrorMessage = "Minino é 3")]
        [MaxLength(100, ErrorMessage = "Maximo é 100")]
        public string Description { get;  set; } = string.Empty;



        [Required(ErrorMessage = "Informe o preço")]
        public decimal Price { get;  set; }


        // Propriedades de relacionamento
        public int CategoryId { get; set; }
        // public Category? Category { get; set; }   
    }
}