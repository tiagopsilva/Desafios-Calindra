using Calindra.Desafio.Domain.Commands.Inputs;
using Calindra.Desafio.Domain.Services;
using Calindra.Desafio.Domain.Tests.Assets;
using Calindra.Desafio.Domain.Tests.Mock;
using Microsoft.Extensions.Configuration;
using RichardSzalay.MockHttp;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Calindra.Desafio.Domain.Tests.Services
{
    public class DistanceCalculatorServiceTests
    {
        private IConfiguration _configuration;

        public DistanceCalculatorServiceTests()
        {
            _configuration = Settings.InitConfiguration();
        }

        [Fact]
        public async Task DadoErroNaRequisicaoDeveRetornarMethodResultFail()
        {
            // Arrange
            var addresses = new Addresses();
            addresses.AddressList.AddRange(AddressResponseFiles.AddressesFiles.Keys);

            var mockHttp = new MockHttpMessageHandler();

            var urlBase = _configuration["GoogleApi:Url:Base"];
            var urlPath = _configuration["GoogleApi:Url:Path"];

            foreach (var address in AddressResponseFiles.AddressesFiles.Keys)
            {
                mockHttp.When($"{urlBase}{urlPath}?address={address}*")
                    .Throw(new HttpRequestException("Falha na requisição"));
            }

            using var httpClient = new HttpClient(mockHttp);

            var googleGeolocationApiService = new GoogleGeolocationApiService(httpClient, _configuration);
            var distanceCalculatorService = new DistanceCalculatorService(new MockLogger<DistanceCalculatorService>() , googleGeolocationApiService);

            // Act
            var result = await distanceCalculatorService.CalculateDistancesFromAddressess(addresses);
            
            // Assert
            Assert.True(result.Failure);
            Assert.False(result.Success);
        }
    }
}
