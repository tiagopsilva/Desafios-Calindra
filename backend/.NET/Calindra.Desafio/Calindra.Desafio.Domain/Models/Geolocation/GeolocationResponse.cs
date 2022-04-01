using System.Collections.Generic;
using Calindra.Desafio.Domain.Models.Geolocation;

namespace Calindra.Desafio.Domain.Models.Geolocation
{
    public class GeolocationResponse
    {
        public GeolocationResponse()
        {
            Results = new List<GeolocationResult>();
        }

        public List<GeolocationResult> Results { get; set; }
        public string Status { get; set; }
    }
}
