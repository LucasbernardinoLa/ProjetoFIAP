using System.ComponentModel.DataAnnotations;
using ProjetoFIAP.Api.Domain.Constants;
using Swashbuckle.AspNetCore.Annotations;

namespace ProjetoFIAP.Api.Application.DTOs
{
    [SwaggerSchema(Title = "Login")]
    public record LoginDTO
    {
        [Required(ErrorMessage = "Email é obrigatório")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        [SwaggerSchema(Description = "Email do usuário")]
        public string Email { get; init; } = string.Empty;

        [Required(ErrorMessage = "Senha é obrigatória")]
        [RegularExpression(ValidationConstants.PasswordRegex, 
            ErrorMessage = ValidationConstants.PasswordErrorMessage)]
        [SwaggerSchema(Description = "Senha do usuário")]
        public string Password { get; init; } = string.Empty;
    }

    [SwaggerSchema(Title = "Login Response")]
    public record LoginResponseDTO
    {
        [SwaggerSchema(Description = "Token de autenticação")]
        public string Token { get; init; } = string.Empty;

        [SwaggerSchema(Description = "Dados do usuário")]
        public UserResponseDTO User { get; init; } = null!;
    }
}