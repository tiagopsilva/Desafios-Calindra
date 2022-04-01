using Calindra.Desafio.Domain.Commands.Handlers;
using Calindra.Desafio.Domain.Commands.Inputs;
using Calindra.Desafio.Domain.Models.Responses;
using Calindra.Desafio.Domain.Services;
using Calindra.Desafio.Domain.Tests.Assets;
using Calindra.Desafio.Domain.Tests.Mock;
using Microsoft.Extensions.Configuration;
using RichardSzalay.MockHttp;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;

namespace Calindra.Desafio.Domain.Tests.Commands.Handlers
{
    public class CalculatorHandlerTests
    {
        private readonly IConfiguration _configuration;

        public CalculatorHandlerTests()
        {
            _configuration = Settings.InitConfiguration();
        }

        [Fact]
        public async Task DadosEnderecosValidosDeveRetornarLocalizacoesComRelacionamentoDeDistanciasComEnderecosProximosEDistantes()
        {
            // Arrange
            var addresses = new Addresses();
            addresses.AddressList.AddRange(AddressResponseFiles.AddressesFiles.Keys);

            var mockHttp = new MockHttpMessageHandler();

            var urlBase = _configuration["GoogleApi:Url:Base"];
            var urlPath = _configuration["GoogleApi:Url:Path"];
            var accessKey = _configuration[GoogleGeolocationApiService.GoogleGeolocationAccessKeyEnvironmentVariable];

            var directory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            foreach (var addressItem in AddressResponseFiles.AddressesFiles)
            {
                var json = File.ReadAllText(Path.Combine(directory, "Assets", addressItem.Value));

                mockHttp
                    .When(HttpMethod.Get, $"{urlBase}{urlPath}?address={addressItem.Key}&key={accessKey}")
                    .Respond("application/json", json);
            }

            using var httpClient = new HttpClient(mockHttp);

            var distanceCalculatorService = new DistanceCalculatorService(
                new MockLogger<DistanceCalculatorService>(),
                new GoogleGeolocationApiService(httpClient, _configuration));

            // Act
            var result = await new CalculatorHandler(distanceCalculatorService).Execute(addresses);

            // Assert
            Assert.NotNull(result);

            var points = result.Data as List<GeolocationReferencesInfo>;
            Assert.NotNull(points);
            Assert.Equal(addresses.AddressList.Count, points.Count);

            Assert.Equal(1, points[0].ShorterDistances.Count);
            Assert.Equal(2, points[0].GreatherDistances.Count);

            Assert.Equal(1, points[1].ShorterDistances.Count);
            Assert.Equal(2, points[1].GreatherDistances.Count);

            Assert.Equal(1, points[2].ShorterDistances.Count);
            Assert.Equal(2, points[2].GreatherDistances.Count);
        }
    }
}
