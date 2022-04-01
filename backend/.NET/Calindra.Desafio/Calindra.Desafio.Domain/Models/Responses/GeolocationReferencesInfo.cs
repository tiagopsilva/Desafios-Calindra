using System.Collections.Generic;
using Calindra.Desafio.Domain.Models.Responses;
using Newtonsoft.Json;

namespace Calindra.Desafio.Domain.Models.Responses
{
    public class GeolocationReferencesInfo
    {
        public GeolocationReferencesInfo()
        {
            Addresses = new List<GeolocationInfo>();
        }

        public string PlaceId { get; set; }
        public string Address { get; set; }
        public Location Location { get; set; }
        public IList<GeolocationInfo> GreatherDistances { get; set; }
        public IList<GeolocationInfo> ShorterDistances { get; set; }

        [JsonIgnore]
        public IList<GeolocationInfo> Addresses { get; set; }           
    }
}
