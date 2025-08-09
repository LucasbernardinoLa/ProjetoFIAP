using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjetoFIAP.Api.Application.DTOs;
using ProjetoFIAP.Api.Infra.Repository.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace ProjetoFIAP.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [SwaggerTag("Gerenciamento de jogos")]
    public class GamesController : ControllerBase
    {
        private readonly IGameRepository _gameService;

        public GamesController(IGameRepository gameService)
        {
            _gameService = gameService;
        }

        [HttpGet]
        [Authorize]
        [SwaggerOperation(Summary = "Listar jogos", Description = "Retorna todos os jogos cadastrados")]
        [SwaggerResponse(200, "Lista de jogos retornada com sucesso", typeof(IEnumerable<GameDTO>))]
        [SwaggerResponse(401, "Não autorizado")]
        public async Task<ActionResult<IEnumerable<GameDTO>>> GetAll()
        {
            var games = await _gameService.GetAllGamesAsync();
            return Ok(games);
        }

        [HttpPost]
        [Authorize(Policy = "Admin")]
        [SwaggerOperation(Summary = "Cadastrar jogo", Description = "Cadastra um novo jogo (requer permissão de Admin)")]
        [SwaggerResponse(201, "Jogo criado com sucesso", typeof(GameDTO))]
        [SwaggerResponse(400, "Dados inválidos")]
        [SwaggerResponse(401, "Não autorizado")]
        [SwaggerResponse(403, "Acesso negado - Requer role Admin")]
        public async Task<ActionResult<GameDTO>> Create([FromBody] GameDTO game)
        {
            var newGame = await _gameService.CreateGameAsync(game);
            return CreatedAtAction(nameof(GetById), new { id = newGame.Id }, newGame);
        }

        [HttpGet("{id}")]
        [Authorize]
        [SwaggerOperation(Summary = "Obter jogo por ID",Description = "Retorna um jogo específico pelo seu identificador")]
        [SwaggerResponse(200, "Jogo encontrado com sucesso", typeof(GameDTO))]
        [SwaggerResponse(401, "Não autorizado")]
        [SwaggerResponse(404, "Jogo não encontrado")]
        public async Task<ActionResult<GameDTO>> GetById(Guid id)
        {
            var game = await _gameService.GetGameByIdAsync(id);
            if (game == null) return NotFound();
            return Ok(game);
        }
    }
}