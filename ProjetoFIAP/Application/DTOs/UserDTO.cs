using System.ComponentModel.DataAnnotations;
using ProjetoFIAP.Api.Domain.Constants;
using Swashbuckle.AspNetCore.Annotations;

namespace ProjetoFIAP.Api.Application.DTOs
{
    [SwaggerSchema(Title = "User Registration")]
    public record UserRegistrationDTO
    {
        [Required(ErrorMessage = "Nome é obrigatório")]
        [StringLength(100, ErrorMessage = "Nome deve ter no máximo 100 caracteres")]
        [SwaggerSchema(Description = "Nome do usuário")]
        public string Name { get; init; } = string.Empty;

        [Required(ErrorMessage = "Email é obrigatório")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        [StringLength(255, ErrorMessage = "Email deve ter no máximo 255 caracteres")]
        [SwaggerSchema(Description = "Email do usuário")]
        public string Email { get; init; } = string.Empty;

        [Required(ErrorMessage = "Senha é obrigatória")]
        [RegularExpression(ValidationConstants.PasswordRegex, 
            ErrorMessage = ValidationConstants.PasswordErrorMessage)]
        [SwaggerSchema(Description = "Senha do usuário")]
        public string Password { get; init; } = string.Empty;
    }

    [SwaggerSchema(Title = "User Response")]
    public record UserResponseDTO
    {
        [SwaggerSchema(Description = "Identificador único do usuário")]
        public Guid Id { get; init; }

        [SwaggerSchema(Description = "Nome do usuário")]
        public string Name { get; init; } = string.Empty;

        [SwaggerSchema(Description = "Email do usuário")]
        public string Email { get; init; } = string.Empty;

        [SwaggerSchema(Description = "Papel do usuário (Admin/User)")]
        public string Role { get; init; } = string.Empty;
    }
}