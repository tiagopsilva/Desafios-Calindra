using Calindra.Desafio.Domain.Helpers;
using Calindra.Desafio.Domain.Models.Geolocation;
using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Calindra.Desafio.Domain.Services
{
    public class GoogleGeolocationApiService : IGeolocationApiService
    {
        public const string GoogleGeolocationAccessKeyEnvironmentVariable = "GOOGLE_GEOLOCATION_ACCESS_KEY";

        private readonly string UrlPath;
        private readonly string AccessKey;
        private readonly HttpClient _httpClient;

        public GoogleGeolocationApiService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(configuration["GoogleApi:Url:Base"]);
            UrlPath = configuration["GoogleApi:Url:Path"];
            AccessKey = configuration[GoogleGeolocationAccessKeyEnvironmentVariable];
        }

        public async Task<GeolocationResponse> GetGeolocationFrom(string endereco)
        {
            var urlPath = UrlPath + $"?address={endereco}&key={AccessKey}";
            var response = await _httpClient.GetAsync(urlPath);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Deserializers.DeserializeJsonAsSnakeCaseNaming<GeolocationResponse>(content);
            }
            else
            {
                return null;
            }
        }
    }
}
