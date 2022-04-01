using Calindra.Desafio.Domain.Helpers;
using Calindra.Desafio.Domain.Models.Geolocation;
using Calindra.Desafio.Domain.Services;
using Calindra.Desafio.Domain.Tests.Assets;
using Microsoft.Extensions.Configuration;
using RichardSzalay.MockHttp;
using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;

namespace Calindra.Desafio.Domain.Tests.Services
{
    public class GoogleGeolocationApiServiceTests
    {
        private readonly IConfiguration _configuration;

        public GoogleGeolocationApiServiceTests()
        {
            _configuration = Settings.InitConfiguration();
        }

        [Fact]
        public async Task DadoEnderecoValidoDeveRetornarGeolocalizacao()
        {
            // Arrange
            var address = AddressResponseFiles.AddressesFiles.Keys.Where(x => x.Contains("Caixa Economica", StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            var fileName = AddressResponseFiles.AddressesFiles[address];
            var directory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var json = File.ReadAllText(Path.Combine(directory, "Assets", fileName));

            var mockHttp = new MockHttpMessageHandler();
            var urlBase = _configuration["GoogleApi:Url:Base"];
            var urlPath = _configuration["GoogleApi:Url:Path"];
            mockHttp.When($"{urlBase}{urlPath}*")
                .Respond("application/json", json);

            using var httpClient = new HttpClient(mockHttp);

            var service = new GoogleGeolocationApiService(httpClient, _configuration);

            // Act
            var result = await service.GetGeolocationFrom(address);

            // Assert
            var expected = Deserializers.DeserializeJsonAsSnakeCaseNaming<GeolocationResponse>(json);
            Assert.Equal(expected.Status, result.Status);
            Assert.Equal(expected.Results.Count, result.Results.Count);
            Assert.Equal(expected.Results.First().PlaceId, result.Results.First().PlaceId);
            Assert.Equal(expected.Results.First().FormattedAddress, result.Results.First().FormattedAddress);
            Assert.Equal(expected.Results.First().Geometry.Location.Lat, result.Results.First().Geometry.Location.Lat);
            Assert.Equal(expected.Results.First().Geometry.Location.Lng, result.Results.First().Geometry.Location.Lng);
        }
    }
}
