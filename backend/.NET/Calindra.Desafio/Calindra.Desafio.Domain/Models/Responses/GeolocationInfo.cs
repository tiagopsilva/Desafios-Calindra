namespace Calindra.Desafio.Domain.Models.Responses
{
    public class GeolocationInfo
    {
        public GeolocationInfo(GeolocationReferencesInfo point, double distance)
        {
            PlaceId = point.PlaceId;
            Address = point.Address;
            Location = point.Location;
            Distance = distance;
        }

        public string PlaceId { get; set; }
        public string Address { get; set; }
        public Location Location { get; set; }
        public double Distance { get; set; }
    }
}
