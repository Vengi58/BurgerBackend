using System.Collections.Generic;

namespace BurgerBackend.Services
{
    public interface IGeoService
    {
        Coordinates GeoLocation();
        Coordinates GeoCoding(string country, string city, string street, int number, string postCode);
        int GeoDistance(Coordinates origin, Coordinates destination);
        List<int> GeoDistances(Coordinates origin, List<Coordinates> destinations);
    }
    public struct Coordinates
    {
        public double GLat { get; set; }
        public double GLong { get; set; }
    }
}
