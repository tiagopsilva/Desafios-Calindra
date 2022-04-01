using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Calindra.Desafio.Domain.Tests.Extensions
{
    public class ListExtensionsTests
    {
        [Fact]
        public void DadoDuasListasAddRangeDevePassarValoresDeUmaListaParaOutra()
        {
            // Arrange
            var listDest = new List<int>();
            var listOrig = new List<int> { 1, 2, 3, 4 };

            // Act
            listDest.AddRange(listOrig);

            // Assert
            Assert.Equal(listDest.Count, listOrig.Count);
            Assert.True(listOrig.All(x => listDest.Contains(x)));
        }
    }
}
