using Shoop.Domain.Validation;

namespace Shoop.Domain.Entities
{
    public class Product : Entity
    {
        // Construtor: Usado para CRIAR um novo produto.
        // É a porta de entrada para garantir que o objeto nasce válido.
        public Product(string title, string description, decimal price)
        {
             // O CategoryId será setado separadamente ou em outro construtor, 
             // mas as propriedades primárias são validadas aqui.
             ValidateDomain(title, description, price);
        }

        // Construtor opcional para uso do EF Core (precisa de um construtor sem parâmetros)
        // Se você não o usar, o EF Core tentará usar o construtor público acima.
        // protected Product() {}


        // Propriedades primárias com 'private set' para garantir que 
        // a mutação só ocorra via MÉTODOS de domínio (Update).
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
            // 1. Validação do Título
            DomainExceptionValidation.When(string.IsNullOrEmpty(title),
                "Título inválido. O título é obrigatório");

            DomainExceptionValidation.When(title.Length < 3,
               "O título deve ter no mínimo 3 caracteres");

            // 2. Validação da Descrição
            DomainExceptionValidation.When(string.IsNullOrEmpty(description),
            "Descrição inválida. A descrição é obrigatória");

            DomainExceptionValidation.When(description.Length < 5, // Aumentei o mínimo
               "A descrição deve ter no mínimo 5 caracteres");

            // 3. Validação do Preço
            DomainExceptionValidation.When(price <= 0,
               "Preço inválido. O preço deve ser maior que zero.");

            // Se todas as validações passarem, atribua os valores.
            Title = title;
            Description = description;
            Price = price;
        }
    }
}