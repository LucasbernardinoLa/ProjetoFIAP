using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

namespace ProjetoFIAP.Api.Application.DTOs
{
    [SwaggerSchema(Title = "Game")]
    public record GameDTO
    {
        [SwaggerSchema(Description = "Identificador único do jogo")]
        public Guid Id { get; init; }

        [Required(ErrorMessage = "Título é obrigatório")]
        [StringLength(100, ErrorMessage = "Título deve ter no máximo 100 caracteres")]
        [SwaggerSchema(Description = "Título do jogo")]
        public string Title { get; init; } = string.Empty;

        [Required(ErrorMessage = "Descrição é obrigatória")]
        [StringLength(500, ErrorMessage = "Descrição deve ter no máximo 500 caracteres")]
        [SwaggerSchema(Description = "Descrição do jogo")]
        public string Description { get; init; } = string.Empty;

        [Required(ErrorMessage = "Preço é obrigatório")]
        [Range(0, double.MaxValue, ErrorMessage = "Preço não pode ser negativo")]
        [SwaggerSchema(Description = "Preço do jogo")]
        public decimal Price { get; init; }

        [Required(ErrorMessage = "Data de lançamento é obrigatória")]
        [SwaggerSchema(Description = "Data de lançamento do jogo")]
        public DateTime ReleaseDate { get; init; }

        [Required(ErrorMessage = "Desenvolvedor é obrigatório")]
        [StringLength(100, ErrorMessage = "Desenvolvedor deve ter no máximo 100 caracteres")]
        [SwaggerSchema(Description = "Empresa desenvolvedora do jogo")]
        public string Developer { get; init; } = string.Empty;

        [Required(ErrorMessage = "Publicadora é obrigatória")]
        [StringLength(100, ErrorMessage = "Publicadora deve ter no máximo 100 caracteres")]
        [SwaggerSchema(Description = "Empresa publicadora do jogo")]
        public string Publisher { get; init; } = string.Empty;
    }
}