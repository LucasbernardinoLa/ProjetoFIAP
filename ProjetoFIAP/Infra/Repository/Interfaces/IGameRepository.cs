using ProjetoFIAP.Api.Application.DTOs;

namespace ProjetoFIAP.Api.Infra.Repository.Interfaces
{
    public interface IGameRepository
    {
        Task<IEnumerable<GameDTO>> GetAllGamesAsync();
        Task<GameDTO> GetGameByIdAsync(Guid id);
        Task<GameDTO> CreateGameAsync(GameDTO gameDto);
    }
}