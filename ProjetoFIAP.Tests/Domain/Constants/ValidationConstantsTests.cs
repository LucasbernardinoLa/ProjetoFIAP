using System.Text.RegularExpressions;
using ProjetoFIAP.Api.Domain.Constants;
using Xunit;

namespace ProjetoFIAP.Tests.Domain.Constants
{
    public class ValidationConstantsTests
    {
        [Theory]
        [InlineData("Test@123", true)]
        [InlineData("Password@123", true)]
        [InlineData("Complex!2023", true)]
        [InlineData("weak", false)]
        [InlineData("onlylower123!", false)]
        [InlineData("ONLYUPPER123!", false)]
        [InlineData("NoSpecialChar123", false)]
        [InlineData("No@Numbers", false)]
        [InlineData("Short@1", false)]
        public void PasswordRegex_Validation(string password, bool shouldMatch)
        {
            // Arrange & Act
            var isMatch = Regex.IsMatch(password, ValidationConstants.PasswordRegex);

            // Assert
            Assert.Equal(shouldMatch, isMatch);
        }
    }
}