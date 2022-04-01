using System.Threading.Tasks;
using Calindra.Desafio.Domain.Models.Geolocation;

namespace Calindra.Desafio.Domain.Services
{
    public interface IGeolocationApiService
    {
        Task<GeolocationResponse> GetGeolocationFrom(string address);
    }
}
