using Shoop.Domain.Validation;

namespace Shoop.Domain.Entities
{
    public class Product : Entity
    {
        public Product(string title, string description, decimal price)
        {
            ValidateDomain(title, description, price);
        }


        public string Title { get; private set; } = string.Empty;
        public string Description { get; private set; } = string.Empty;
        public decimal Price { get; private set; }


        // Propriedades de relacionamento
        public int CategoryId { get; set; }
        public Category? Category { get; set; }


        // Método para ATUALIZAR o estado do produto, 
        // garantindo que as novas informações são válidas.
        public void Update(string title, string description, decimal price, int categoryId)
        {
            ValidateDomain(title, description, price);

            // Validações adicionais para CategoryId
            DomainExceptionValidation.When(categoryId <= 0,
                "CategoryId inválido. O CategoryId deve ser positivo");

            CategoryId = categoryId;
        }


        // Método central que aplica todas as regras de validação.
        private void ValidateDomain(string title, string description, decimal price)
        {

            DomainExceptionValidation.When(string.IsNullOrEmpty(title),
                "Título inválido. O título é obrigatório");

            DomainExceptionValidation.When(title.Length < 3,
               "O título deve ter no mínimo 3 caracteres");


            DomainExceptionValidation.When(string.IsNullOrEmpty(description),
            "Descrição inválida. A descrição é obrigatória");

            DomainExceptionValidation.When(description.Length < 5,
               "A descrição deve ter no mínimo 5 caracteres");


            DomainExceptionValidation.When(price <= 0,
               "Preço inválido. O preço deve ser maior que zero.");

            Title = title;
            Description = description;
            Price = price;
        }
    }
}