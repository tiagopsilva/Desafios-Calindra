namespace Calindra.Desafio.Domain.Models.Geolocation
{
    public class GeolocationResult
    {
        public string PlaceId { get; set; }
        public string FormattedAddress { get; set; }
        public GeolocationGeometry Geometry { get; set; }
    }
}
