using Calindra.Desafio.Domain.Extensions;
using System.Collections.Generic;
using Xunit;

namespace Calindra.Desafio.Domain.Tests.Extensions
{
    public class EnumerableExtensionsTests
    {
        [Fact]
        public void DadoListaPreenchidaIsEmptyDeveRetornarFalse()
        {
            // Arrange
            var list = new List<int> { 1, 3, 5, 7, 9 };

            // Act
            var result = list.IsEmpty();

            // Assert
            Assert.False(result);
        }
        
        [Fact]
        public void DadoListaVaziaIsEmptyDeveRetornarTrue()
        {
            // Arrange
            var list = new List<int>();

            // Act
            var result = list.IsEmpty();

            // Assert
            Assert.True(result);
        }
        
        [Fact]
        public void DadoListaNulaIsEmptyDeveRetornarTrue()
        {
            // Arrange
            List<int> list = null;

            // Act
            var result = list.IsEmpty();

            // Assert
            Assert.True(result);
        }
    }
}
