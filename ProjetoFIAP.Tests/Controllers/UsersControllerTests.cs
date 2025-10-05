using Microsoft.AspNetCore.Mvc;
using Moq;
using ProjetoFIAP.Api.Application.DTOs;
using ProjetoFIAP.Api.Controllers;
using ProjetoFIAP.Api.Domain.Exceptions;
using ProjetoFIAP.Api.Infra.Repository.Interfaces;
using Xunit;

namespace ProjetoFIAP.Tests.Controllers
{
    public class UsersControllerTests
    {
        private readonly Mock<IUserRepository> _mockUserRepository;
        private readonly UsersController _controller;

        public UsersControllerTests()
        {
            _mockUserRepository = new Mock<IUserRepository>();
            _controller = new UsersController(_mockUserRepository.Object);
        }

        [Fact]
        public async Task Register_WithValidData_ReturnsOkResult()
        {
            // Arrange
            var registration = new UserRegistrationDTO
            {
                Name = "Test User",
                Email = "test@test.com",
                Password = "Test@123"
            };

            var expectedResponse = new UserResponseDTO
            {
                Id = Guid.NewGuid(),
                Name = registration.Name,
                Email = registration.Email,
                Role = "User"
            };

            _mockUserRepository
                .Setup(x => x.RegisterUserAsync(registration))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _controller.Register(registration);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<UserResponseDTO>(okResult.Value);
            Assert.Equal(expectedResponse.Email, returnValue.Email);
        }

        [Fact]
        public async Task Register_WithInvalidData_ReturnsBadRequest()
        {
            // Arrange
            var registration = new UserRegistrationDTO
            {
                Name = "Test User",
                Email = "invalid-email",
                Password = "123"
            };

            _mockUserRepository
                .Setup(x => x.RegisterUserAsync(registration))
                .ThrowsAsync(new DomainValidationException("Email inválido"));

            // Act
            var result = await _controller.Register(registration);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal("Email inválido", badRequestResult.Value);
        }

        [Fact]
        public async Task GetAll_WithAdminUser_ReturnsAllUsers()
        {
            // Arrange
            var expectedUsers = new List<UserResponseDTO>
            {
                new() { Id = Guid.NewGuid(), Name = "User 1", Email = "user1@test.com", Role = "User" },
                new() { Id = Guid.NewGuid(), Name = "User 2", Email = "user2@test.com", Role = "Admin" }
            };

            _mockUserRepository
                .Setup(x => x.GetAllUsersAsync())
                .ReturnsAsync(expectedUsers);

            // Act
            var result = await _controller.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<List<UserResponseDTO>>(okResult.Value);
            Assert.Equal(2, returnValue.Count);
        }
    }
}