using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

namespace ProjetoFIAP.Api.Application.DTOs
{
    [SwaggerSchema(Title = "Game")]
    public record GameDTO
    {
        [SwaggerSchema(Description = "Identificador �nico do jogo")]
        public Guid Id { get; init; }

        [Required(ErrorMessage = "T�tulo � obrigat�rio")]
        [StringLength(100, ErrorMessage = "T�tulo deve ter no m�ximo 100 caracteres")]
        [SwaggerSchema(Description = "T�tulo do jogo")]
        public string Title { get; init; } = string.Empty;

        [Required(ErrorMessage = "Descri��o � obrigat�ria")]
        [StringLength(500, ErrorMessage = "Descri��o deve ter no m�ximo 500 caracteres")]
        [SwaggerSchema(Description = "Descri��o do jogo")]
        public string Description { get; init; } = string.Empty;

        [Required(ErrorMessage = "Pre�o � obrigat�rio")]
        [Range(0, double.MaxValue, ErrorMessage = "Pre�o n�o pode ser negativo")]
        [SwaggerSchema(Description = "Pre�o do jogo")]
        public decimal Price { get; init; }

        [Required(ErrorMessage = "Data de lan�amento � obrigat�ria")]
        [SwaggerSchema(Description = "Data de lan�amento do jogo")]
        public DateTime ReleaseDate { get; init; }

        [Required(ErrorMessage = "Desenvolvedor � obrigat�rio")]
        [StringLength(100, ErrorMessage = "Desenvolvedor deve ter no m�ximo 100 caracteres")]
        [SwaggerSchema(Description = "Empresa desenvolvedora do jogo")]
        public string Developer { get; init; } = string.Empty;

        [Required(ErrorMessage = "Publicadora � obrigat�ria")]
        [StringLength(100, ErrorMessage = "Publicadora deve ter no m�ximo 100 caracteres")]
        [SwaggerSchema(Description = "Empresa publicadora do jogo")]
        public string Publisher { get; init; } = string.Empty;
    }
}