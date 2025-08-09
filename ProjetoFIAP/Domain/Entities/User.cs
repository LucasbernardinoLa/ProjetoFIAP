using ProjetoFIAP.Api.Domain.Enums;
using ProjetoFIAP.Api.Domain.Exceptions;

namespace ProjetoFIAP.Api.Domain.Entities
{ 
    public class User
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
        public string PasswordHash { get; private set; } = string.Empty;    
        public UserRole Role { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }

        protected User() { } // Para EF Core

        public User(string name, string email, string passwordHash, UserRole role = UserRole.User)
        {
            ValidateEmail(email);
            ValidatePassword(passwordHash);
            
            Id = Guid.NewGuid();
            Name = name;
            Email = email.ToLower();
            PasswordHash = passwordHash;
            Role = role;
            CreatedAt = DateTime.UtcNow;
        }

        private static void ValidateEmail(string email)
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(email, 
                @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                throw new DomainValidationException("Email inválido");
        }

        private static void ValidatePassword(string password)
        {
            if (string.IsNullOrEmpty(password) || password.Length < 8 ||
                !password.Any(char.IsUpper) ||
                !password.Any(char.IsLower) ||
                !password.Any(char.IsDigit) ||
                !password.Any(c => !char.IsLetterOrDigit(c)))
            {
                throw new DomainValidationException("Senha deve ter no mínimo 8 caracteres, " +
                    "contendo letras maiúsculas, minúsculas, números e caracteres especiais");
            }
        }
    }
}