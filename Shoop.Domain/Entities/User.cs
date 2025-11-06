using Shoop.Domain.Validation;

namespace Shoop.Domain.Entities
{
    public class User : Entity
    {
        
        public User(string username, string password, string role)
        {
            ValidateDomain(username, password, role);
        }

      
        public string Username { get; private set; } = string.Empty;
        public string Password { get; private set; } = string.Empty;
        public string Role { get; private set; } = string.Empty;

        
        // Método de atualização (exemplo: para mudar o papel/role do usuário)
        public void UpdateRole(string role)
        {
            // Reutiliza a validação, focando apenas no Role neste caso
            ValidateRole(role);
        }
        
        // Método para validar o Role (Pode ser chamado internamente)
        private void ValidateRole(string role)
        {
             // 3. Validação do Role
            DomainExceptionValidation.When(string.IsNullOrEmpty(role),
                "Role inválido. O Role é obrigatório");
            
            DomainExceptionValidation.When(role.Length < 3,
               "O Role deve ter no mínimo 3 caracteres");
               
            Role = role;
        }


        private void ValidateDomain(string username, string password, string role)
        {
           
            DomainExceptionValidation.When(string.IsNullOrEmpty(username),
                "Username inválido. O Username é obrigatório");
                
            DomainExceptionValidation.When(username.Length < 4,
               "O Username deve ter no mínimo 4 caracteres");


            DomainExceptionValidation.When(string.IsNullOrEmpty(password),
                "Password inválida. A Password é obrigatória");

            DomainExceptionValidation.When(password.Length < 6,
               "A Password deve ter no mínimo 6 caracteres");

            ValidateRole(role); 


            Username = username;
            Password = password;
        }
    }
}