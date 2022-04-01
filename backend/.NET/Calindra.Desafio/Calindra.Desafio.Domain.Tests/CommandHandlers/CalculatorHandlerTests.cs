using Calindra.Desafio.Domain.Commands.Handlers;
using Calindra.Desafio.Domain.Commands.Inputs;
using Calindra.Desafio.Domain.Models.Responses;
using Calindra.Desafio.Domain.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Calindra.Desafio.Domain.Tests.CommandHandlers
{
    public class CalculatorHandlerTests
    {
        [Fact]
        public async Task DadosEnderecosValidosDeveRetornarLocalizacoesComRelacionamentoDeDistanciasComEnderecosProximosEDistantes()
        {
            // Arrange
            var address = new Addresses();
            address.AddressList.AddRange(new[]
            {
                "Caixa economica federal, retiro, volta redonda rj",
                "Rua 1, Santa Rita de Cássia, barra mansa rj",
                "Santa Cruz, Volta Redonda, RJ",
                "Faetec, Santo Agostinho, Volta Redonda, RJ"
            });

            var googleGeolocationApiService = new GoogleGeolocationApiService();
            var distanceCalculatorService = new DistanceCalculatorService(null, googleGeolocationApiService);
            var handler = new CalculatorHandler(distanceCalculatorService);

            // Act
            var result = await handler.Execute(address);

            // Assert
            Assert.NotNull(result);

            var points = result.Data as List<GeolocationReferencesInfo>;
            Assert.NotNull(points);
            Assert.Equal(address.AddressList.Count, points.Count);

            Assert.Equal(1, points[0].ShorterDistances.Count);
            Assert.Equal(2, points[0].GreatherDistances.Count);
            
            Assert.Equal(1, points[1].ShorterDistances.Count);
            Assert.Equal(2, points[1].GreatherDistances.Count);

            Assert.Equal(1, points[2].ShorterDistances.Count);
            Assert.Equal(2, points[2].GreatherDistances.Count);
        }
    }
}
