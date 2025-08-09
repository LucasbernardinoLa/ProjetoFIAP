using System.ComponentModel.DataAnnotations;
using ProjetoFIAP.Api.Domain.Constants;
using Swashbuckle.AspNetCore.Annotations;

namespace ProjetoFIAP.Api.Application.DTOs
{
    [SwaggerSchema(Title = "Login")]
    public record LoginDTO
    {
        [Required(ErrorMessage = "Email � obrigat�rio")]
        [EmailAddress(ErrorMessage = "Email inv�lido")]
        [SwaggerSchema(Description = "Email do usu�rio")]
        public string Email { get; init; } = string.Empty;

        [Required(ErrorMessage = "Senha � obrigat�ria")]
        [RegularExpression(ValidationConstants.PasswordRegex, 
            ErrorMessage = ValidationConstants.PasswordErrorMessage)]
        [SwaggerSchema(Description = "Senha do usu�rio")]
        public string Password { get; init; } = string.Empty;
    }

    [SwaggerSchema(Title = "Login Response")]
    public record LoginResponseDTO
    {
        [SwaggerSchema(Description = "Token de autentica��o")]
        public string Token { get; init; } = string.Empty;

        [SwaggerSchema(Description = "Dados do usu�rio")]
        public UserResponseDTO User { get; init; } = null!;
    }
}