using Calindra.Desafio.Domain.Results;
using Xunit;

namespace Calindra.Desafio.Domain.Tests.Results
{
    public class MethodResultGenericTests
    {
        [Fact]
        public void DadoTipoDeDadoDeveRetornarMesmoTipoEmData()
        {
            // Arrange
            var methodResult = new MethodResult<MethodResult>(new MethodResult());

            // Act
            // Arrange
            Assert.Equal(typeof(MethodResult), methodResult.Data.GetType());
        }
    }
}
