using System.ComponentModel.DataAnnotations;
using ProjetoFIAP.Api.Domain.Constants;
using Swashbuckle.AspNetCore.Annotations;

namespace ProjetoFIAP.Api.Application.DTOs
{
    [SwaggerSchema(Title = "User Registration")]
    public record UserRegistrationDTO
    {
        [Required(ErrorMessage = "Nome � obrigat�rio")]
        [StringLength(100, ErrorMessage = "Nome deve ter no m�ximo 100 caracteres")]
        [SwaggerSchema(Description = "Nome do usu�rio")]
        public string Name { get; init; } = string.Empty;

        [Required(ErrorMessage = "Email � obrigat�rio")]
        [EmailAddress(ErrorMessage = "Email inv�lido")]
        [StringLength(255, ErrorMessage = "Email deve ter no m�ximo 255 caracteres")]
        [SwaggerSchema(Description = "Email do usu�rio")]
        public string Email { get; init; } = string.Empty;

        [Required(ErrorMessage = "Senha � obrigat�ria")]
        [RegularExpression(ValidationConstants.PasswordRegex, 
            ErrorMessage = ValidationConstants.PasswordErrorMessage)]
        [SwaggerSchema(Description = "Senha do usu�rio")]
        public string Password { get; init; } = string.Empty;
    }

    [SwaggerSchema(Title = "User Response")]
    public record UserResponseDTO
    {
        [SwaggerSchema(Description = "Identificador �nico do usu�rio")]
        public Guid Id { get; init; }

        [SwaggerSchema(Description = "Nome do usu�rio")]
        public string Name { get; init; } = string.Empty;

        [SwaggerSchema(Description = "Email do usu�rio")]
        public string Email { get; init; } = string.Empty;

        [SwaggerSchema(Description = "Papel do usu�rio (Admin/User)")]
        public string Role { get; init; } = string.Empty;
    }
}