using Shoop.Domain.Validation;

namespace Shoop.Domain.Entities
{
    public class Category : Entity
    {
        public Category(string title)
        {
            ValidateDomain(title);
        }

        public string Title { get; private set; } = string.Empty;



        private void ValidateDomain(string title)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(title),
                "title inválido. O title é obrigatório");

            DomainExceptionValidation.When(title.Length < 3,
               "O title deve ter no mínimo 3 caracteres");

            Title = title;
        }

    }
}