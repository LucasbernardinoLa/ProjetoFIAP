using ProjetoFIAP.Api.Application.DTOs;
using ProjetoFIAP.Api.Domain.Entities;
using ProjetoFIAP.Api.Domain.Exceptions;
using Microsoft.EntityFrameworkCore;
using ProjetoFIAP.Api.Infra.Data;
using ProjetoFIAP.Api.Infra.Repository.Interfaces;


namespace ProjetoFIAP.Api.Infra.Repository
{
    public class GameRepository : IGameRepository
    {
        private readonly AppDbContext _context;

        public GameRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<GameDTO> CreateGameAsync(GameDTO gameDto)
        {
            ValidateGame(gameDto);

            var game = new Game(
                gameDto.Title,
                gameDto.Description,
                gameDto.Price,
                gameDto.ReleaseDate,
                gameDto.Developer,
                gameDto.Publisher
            );

            _context.Games.Add(game);
            await _context.SaveChangesAsync();

            return new GameDTO
            {
                Id = game.Id,
                Title = game.Title,
                Description = game.Description,
                Price = game.Price,
                ReleaseDate = game.ReleaseDate,
                Developer = game.Developer,
                Publisher = game.Publisher
            };
        }

        public async Task<IEnumerable<GameDTO>> GetAllGamesAsync()
        {
            return await _context.Games
                .Select(g => new GameDTO
                {
                    Id = g.Id,
                    Title = g.Title,
                    Description = g.Description,
                    Price = g.Price,
                    ReleaseDate = g.ReleaseDate,
                    Developer = g.Developer,
                    Publisher = g.Publisher
                })
                .ToListAsync();
        }

        public async Task<GameDTO?> GetGameByIdAsync(Guid id)
        {
            var game = await _context.Games.FindAsync(id);
            if (game == null) return null;

            return new GameDTO
            {
                Id = game.Id,
                Title = game.Title,
                Description = game.Description,
                Price = game.Price,
                ReleaseDate = game.ReleaseDate,
                Developer = game.Developer,
                Publisher = game.Publisher
            };
        }

        private static void ValidateGame(GameDTO game)
        {
            if (string.IsNullOrWhiteSpace(game.Title))
                throw new DomainValidationException("Título do jogo é obrigatório");

            if (game.Price < 0)
                throw new DomainValidationException("Preço não pode ser negativo");

            if (game.ReleaseDate > DateTime.UtcNow)
                throw new DomainValidationException("Data de lançamento não pode ser futura");

            if (string.IsNullOrWhiteSpace(game.Developer))
                throw new DomainValidationException("Desenvolvedor é obrigatório");

            if (string.IsNullOrWhiteSpace(game.Publisher))
                throw new DomainValidationException("Publicadora é obrigatória");
        }
    }
}