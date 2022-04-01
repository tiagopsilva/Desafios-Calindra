namespace Calindra.Desafio.Domain.Models.Responses
{
    public class Location
    {
        public Location(double lat, double lng)
        {
            Lat = lat;
            Lng = lng;
        }

        public double Lat { get; set; }
        public double Lng { get; set; }
    }
}
