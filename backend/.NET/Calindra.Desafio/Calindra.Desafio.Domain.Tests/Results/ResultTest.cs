using Calindra.Desafio.Domain.Results;
using System.Linq;
using Xunit;

namespace Calindra.Desafio.Domain.Tests.Results
{
    public class ResultTest
    {
        [Fact]
        public void DadoOkDeveRetornarMethodResultSemErrosEValido()
        {
            // Arrange
            var methodResult = Result.Ok();

            // Act
            // Assert
            Assert.True(methodResult.Success);
            Assert.False(methodResult.Failure);
            Assert.Empty(methodResult.Failures);
        }

        [Fact]
        public void DadoFailDeveRetornarMethodResultComErroEInvalido()
        {
            // Arrange
            var methodResult = Result.Fail(string.Empty, "Mensagem de erro");

            // Act
            // Assert
            Assert.True(methodResult.Failure);
            Assert.False(methodResult.Success);
            Assert.Equal(1, methodResult.Failures.Count);
        }

        [Fact]
        public void DadoFailSemErrosDeveRetornarMethodResultComErroPadrao()
        {
            // Arrange
            var methodResult = Result.Fail(string.Empty, null);

            // Act
            // Assert
            Assert.True(methodResult.Failure);
            Assert.False(methodResult.Success);
            Assert.Equal(1, methodResult.Failures.Count);
            Assert.Equal(1, methodResult.Failures.First().Messages.Count);
        }
    }
}
