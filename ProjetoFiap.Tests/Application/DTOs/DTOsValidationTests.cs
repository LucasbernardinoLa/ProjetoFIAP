using System.ComponentModel.DataAnnotations;
using ProjetoFIAP.Api.Application.DTOs;
using Xunit;

namespace ProjetoFIAP.Tests.Application.DTOs
{
    public class DTOsValidationTests
    {
        [Theory]
        [InlineData("test@email.com", "Test@123", true)]
        [InlineData("", "Test@123", false)]
        [InlineData("invalid", "Test@123", false)]
        [InlineData("test@email.com", "", false)]
        [InlineData("test@email.com", "weak", false)]
        public void LoginDTO_Validation(string email, string password, bool shouldBeValid)
        {
            // Arrange
            var dto = new LoginDTO
            {
                Email = email,
                Password = password
            };
            var context = new ValidationContext(dto);
            var results = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(dto, context, results, true);

            // Assert
            Assert.Equal(shouldBeValid, isValid);
            if (!shouldBeValid)
                Assert.NotEmpty(results);
        }

        [Theory]
        [InlineData("Test User", "test@email.com", "Test@123", true)]
        [InlineData("", "test@email.com", "Test@123", false)]
        [InlineData("Test User", "", "Test@123", false)]
        [InlineData("Test User", "invalid", "Test@123", false)]
        [InlineData("Test User", "test@email.com", "", false)]
        [InlineData("Test User", "test@email.com", "weak", false)]
        public void UserRegistrationDTO_Validation(string name, string email, string password, bool shouldBeValid)
        {
            // Arrange
            var dto = new UserRegistrationDTO
            {
                Name = name,
                Email = email,
                Password = password
            };
            var context = new ValidationContext(dto);
            var results = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(dto, context, results, true);

            // Assert
            Assert.Equal(shouldBeValid, isValid);
            if (!shouldBeValid)
                Assert.NotEmpty(results);
        }

        [Theory]
        [InlineData("Zelda", "Game description", 299.99, true)]
        [InlineData("", "Game description", 299.99, false)]
        [InlineData("Zelda", "", 299.99, false)]
        [InlineData("Zelda", "Game description", -1, false)]
        public void GameDTO_Validation(string title, string description, decimal price, bool shouldBeValid)
        {
            // Arrange
            var dto = new GameDTO
            {
                Title = title,
                Description = description,
                Price = price,
                ReleaseDate = DateTime.UtcNow,
                Developer = "Nintendo",
                Publisher = "Nintendo"
            };
            var context = new ValidationContext(dto);
            var results = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(dto, context, results, true);

            // Assert
            Assert.Equal(shouldBeValid, isValid);
            if (!shouldBeValid)
                Assert.NotEmpty(results);
        }
    }
}