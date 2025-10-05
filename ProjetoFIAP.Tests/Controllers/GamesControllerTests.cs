using Microsoft.AspNetCore.Mvc;
using Moq;
using ProjetoFIAP.Api.Application.DTOs;
using ProjetoFIAP.Api.Controllers;
using ProjetoFIAP.Api.Infra.Repository.Interfaces;
using Xunit;

namespace ProjetoFIAP.Tests.Controllers
{
    public class GamesControllerTests
    {
        private readonly Mock<IGameRepository> _mockGameRepository;
        private readonly GamesController _controller;

        public GamesControllerTests()
        {
            _mockGameRepository = new Mock<IGameRepository>();
            _controller = new GamesController(_mockGameRepository.Object);
        }

        [Fact]
        public async Task GetAll_ReturnsAllGames()
        {
            // Arrange
            var expectedGames = new List<GameDTO>
            {
                new() 
                { 
                    Id = Guid.NewGuid(),
                    Title = "Test Game 1",
                    Price = 59.99m,
                    Developer = "Test Dev"
                },
                new() 
                { 
                    Id = Guid.NewGuid(),
                    Title = "Test Game 2",
                    Price = 69.99m,
                    Developer = "Test Dev"
                }
            };

            _mockGameRepository
                .Setup(x => x.GetAllGamesAsync())
                .ReturnsAsync(expectedGames);

            // Act
            var result = await _controller.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<List<GameDTO>>(okResult.Value);
            Assert.Equal(2, returnValue.Count);
        }

        [Fact]
        public async Task GetById_WithExistingId_ReturnsGame()
        {
            // Arrange
            var gameId = Guid.NewGuid();
            var expectedGame = new GameDTO
            {
                Id = gameId,
                Title = "Test Game",
                Price = 59.99m,
                Developer = "Test Dev"
            };

            _mockGameRepository
                .Setup(x => x.GetGameByIdAsync(gameId))
                .ReturnsAsync(expectedGame);

            // Act
            var result = await _controller.GetById(gameId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<GameDTO>(okResult.Value);
            Assert.Equal(gameId, returnValue.Id);
        }

        [Fact]
        public async Task Create_WithValidData_ReturnsCreatedGame()
        {
            // Arrange
            var newGame = new GameDTO
            {
                Title = "New Game",
                Price = 59.99m,
                Developer = "Test Dev"
            };

            var createdGame = new GameDTO
            {
                Id = Guid.NewGuid(),
                Title = newGame.Title,
                Price = newGame.Price,
                Developer = newGame.Developer
            };

            _mockGameRepository
                .Setup(x => x.CreateGameAsync(newGame))
                .ReturnsAsync(createdGame);

            // Act
            var result = await _controller.Create(newGame);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var returnValue = Assert.IsType<GameDTO>(createdAtActionResult.Value);
            Assert.Equal(newGame.Title, returnValue.Title);
        }
    }
}