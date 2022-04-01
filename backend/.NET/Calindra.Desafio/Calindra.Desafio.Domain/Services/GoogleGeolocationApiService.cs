using Calindra.Desafio.Domain.Models.Geolocation;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Calindra.Desafio.Domain.Services
{
    public class GoogleGeolocationApiService : IDisposable, IGeolocationApiService
    {
        private static readonly string AccessKey = Environment.GetEnvironmentVariable("GOOGLE_GEOLOCATION_ACCESS_KEY");
        
        private const string HostUrl = "https://maps.googleapis.com";
        private const string UrlPath = "/maps/api/geocode/json";

        private HttpClient _httpClient;

        public GoogleGeolocationApiService()
        {
            _httpClient = new HttpClient { BaseAddress = new Uri(HostUrl) };
        }

        public async Task<GeolocationResponse> GetGeolocationFrom(string endereco)
        {
            var urlPath = UrlPath + $"?address={endereco}&key={AccessKey}";
            var response = await _httpClient.GetAsync(urlPath);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<GeolocationResponse>(content, new JsonSerializerSettings
                {
                    ContractResolver = new DefaultContractResolver
                    {
                        NamingStrategy = new SnakeCaseNamingStrategy { ProcessDictionaryKeys = true }
                    }
                });
            }
            else
            {
                return null;
            }
        }

        public void Dispose()
        {
            _httpClient?.Dispose();
            _httpClient = null;
            GC.SuppressFinalize(this);
        }
    }
}
