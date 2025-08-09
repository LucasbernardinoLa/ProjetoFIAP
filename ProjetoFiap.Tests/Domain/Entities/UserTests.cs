using ProjetoFIAP.Api.Domain.Entities;
using ProjetoFIAP.Api.Domain.Enums;
using ProjetoFIAP.Api.Domain.Exceptions;
using Xunit;

namespace ProjetoFIAP.Tests.Domain.Entities
{
    public class UserTests
    {
        [Fact]
        public void CreateUser_WithValidData_ShouldCreateSuccessfully()
        {
            // Arrange
            var name = "Test User";
            var email = "test@email.com";
            var password = "Test@123";
            var role = UserRole.User;

            // Act
            var user = new User(name, email, password, role);

            // Assert
            Assert.NotNull(user);
            Assert.Equal(name, user.Name);
            Assert.Equal(email.ToLower(), user.Email);
            Assert.Equal(role, user.Role);
            Assert.NotEqual(default, user.Id);
            Assert.NotEqual(default, user.CreatedAt);
        }

        [Theory]
        [InlineData("")]
        [InlineData("invalid")]
        [InlineData("invalid@")]
        [InlineData("@invalid.com")]
        public void CreateUser_WithInvalidEmail_ShouldThrowDomainValidationException(string invalidEmail)
        {
            // Arrange
            var name = "Test User";
            var password = "Test@123";

            // Act & Assert
            var exception = Assert.Throws<DomainValidationException>(() => 
                new User(name, invalidEmail, password));
            Assert.Equal("Email inválido", exception.Message);
        }

        [Theory]
        [InlineData("")]
        [InlineData("short")]
        [InlineData("onlylowercase")]
        [InlineData("ONLYUPPERCASE")]
        [InlineData("NoSpecialChar1")]
        [InlineData("no@numbers")]
        public void CreateUser_WithInvalidPassword_ShouldThrowDomainValidationException(string invalidPassword)
        {
            // Arrange
            var name = "Test User";
            var email = "test@email.com";

            // Act & Assert
            var exception = Assert.Throws<DomainValidationException>(() => 
                new User(name, email, invalidPassword));
            Assert.Contains("Senha deve ter", exception.Message);
        }
    }
}