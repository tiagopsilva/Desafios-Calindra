using Calindra.Desafio.Domain.Services;
using System.Threading.Tasks;
using Xunit;

namespace Calindra.Desafio.Domain.Tests.Services
{
    public class GoogleGeolocationApiServiceTests
    {
        [Theory]
        [InlineData("Caixa Economica Federal, Volta Redonda RJ")]
        [InlineData("Av. Rio Branco, 1 Centro, Rio de Janeiro RJ, 20090003")]
        [InlineData("Praça Mal. Âncora, 122 Centro, Rio de Janeiro RJ, 20021200")]
        [InlineData("Rua 19 de Fevereiro, 34 Botafogo, Rio de Janeiro RJ, 22280030")]
        public async Task DadoEnderecoValidoDeveRetornarGeolocalizacao(string endereco)
        {
            // Arrange
            using var service = new GoogleGeolocationApiService();

            // Act
            var result = await service.GetGeolocationFrom(endereco);

            // Assert
            Assert.NotNull(result);
        }
    }
}
