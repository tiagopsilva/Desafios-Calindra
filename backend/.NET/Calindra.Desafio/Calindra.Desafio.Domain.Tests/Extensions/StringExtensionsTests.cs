using Calindra.Desafio.Domain.Extensions;
using Xunit;

namespace Calindra.Desafio.Domain.Tests.Extensions
{
    public class StringExtensionsTests
    {
        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void DadoStringVaziaIsEmptyDeveRetornarTrue(string value)
        {
            // Arrange
            // Act
            var result = value.IsEmpty();

            // Assert
            Assert.True(result);
        }

        [Theory]
        [InlineData("a")]
        [InlineData("abc")]
        public void DadoStringComCaracteresIsEmptyDeveRetornarFalse(string value)
        {
            // Arrange
            // Act
            var result = value.IsEmpty();

            // Assert
            Assert.False(result);
        }

        [Theory]
        [InlineData("a")]
        [InlineData("@")]
        [InlineData("abc 123")]
        public void DadoStringComCaracteresAnyCharDeveRetornarTrue(string value)
        {
            // Arrange
            // Act
            var result = value.AnyChar();

            // Assert
            Assert.True(result);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void DadoStringVaziaAnyCharDeveRetornarFalse(string value)
        {
            // Arrange
            // Act
            var result = value.AnyChar();

            // Assert
            Assert.False(result);
        }
    }
}
