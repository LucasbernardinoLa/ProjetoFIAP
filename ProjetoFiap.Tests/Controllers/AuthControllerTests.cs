using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using ProjetoFIAP.Api.Application.DTOs;
using ProjetoFIAP.Api.Controllers;
using ProjetoFIAP.Api.Infra.Repository.Interfaces;
using Xunit;

namespace ProjetoFIAP.Tests.Controllers
{
    public class AuthControllerTests
    {
        private readonly Mock<IUserRepository> _mockUserRepository;
        private readonly Mock<IConfiguration> _mockConfiguration;
        private readonly Mock<ILogger<AuthController>> _mockLogger;
        private readonly AuthController _controller;

        public AuthControllerTests()
        {
            _mockUserRepository = new Mock<IUserRepository>();
            _mockConfiguration = new Mock<IConfiguration>();
            _mockLogger = new Mock<ILogger<AuthController>>();

            // Setup configuration
            _mockConfiguration.Setup(x => x["Jwt:Key"]).Returns("chave-super-secreta-com-32-caracteres!@");
            _mockConfiguration.Setup(x => x["Jwt:Issuer"]).Returns("ProjetoFIAP");
            _mockConfiguration.Setup(x => x["Jwt:Audience"]).Returns("ProjetoFIAPClient");

            _controller = new AuthController(
                _mockConfiguration.Object,
                _mockUserRepository.Object,
                _mockLogger.Object);
        }

        [Fact]
        public async Task Login_WithValidCredentials_ReturnsOkWithToken()
        {
            // Arrange
            var loginDto = new LoginDTO
            {
                Email = "test@test.com",
                Password = "Test@123"
            };

            var user = new UserResponseDTO
            {
                Id = Guid.NewGuid(),
                Name = "Test User",
                Email = loginDto.Email,
                Role = "User"
            };

            _mockUserRepository
                .Setup(x => x.GetUserByEmailAndPasswordAsync(loginDto.Email, loginDto.Password))
                .ReturnsAsync(user);

            // Act
            var result = await _controller.Login(loginDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var response = Assert.IsType<LoginResponseDTO>(okResult.Value);
            Assert.NotNull(response.Token);
            Assert.Equal(user.Email, response.User.Email);
        }

        [Fact]
        public async Task Login_WithInvalidCredentials_ReturnsUnauthorized()
        {
            // Arrange
            var loginDto = new LoginDTO
            {
                Email = "test@test.com",
                Password = "wrongpassword"
            };

            _mockUserRepository
                .Setup(x => x.GetUserByEmailAndPasswordAsync(loginDto.Email, loginDto.Password))
                .ReturnsAsync((UserResponseDTO?)null);

            // Act
            var result = await _controller.Login(loginDto);

            // Assert
            Assert.IsType<UnauthorizedObjectResult>(result.Result);
        }
    }
}