using Calindra.Desafio.Domain.Results;
using Xunit;

namespace Calindra.Desafio.Domain.Tests.Results
{
    public class FailureTests
    {
        [Fact]
        public void DadoInsercaoDeMensagemVazioOuNulaDeveIgnorar()
        {
            // Arrange
            var failure = new Failure(string.Empty, "");

            // Assert
            failure.Add(string.Empty, string.Empty, null);

            // Arrange
            Assert.Empty(failure.Messages);
        }
    }
}
