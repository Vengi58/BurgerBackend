using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BurgerBackend.Services
{
    public class GoogleGeoService: IGeoService
    {
        private readonly HttpClient httpClient;
        private readonly string ApiKey;

        public GoogleGeoService(string apiKey)
        {
            ApiKey = apiKey;
            httpClient = new HttpClient()
            {
                Timeout = TimeSpan.FromSeconds(30)
            };
        }
        private async Task<T> PostCall<T>(string url)
        {
            var result = httpClient.PostAsync(url, new StringContent("", Encoding.UTF8, "application/json")).Result;
            var json = await result.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(json);
        }

        public Coordinates GeoCoding(string country, string city, string street, int number, string postCode)
        {
            try
            {
                var urlCity = WebUtility.UrlEncode(city);
                var urlCountry = WebUtility.UrlEncode(country);
                var urlStreet = WebUtility.UrlEncode(street);
                var geoCodingData = PostCall<GeoCodingData>($"https://maps.googleapis.com/maps/api/geocode/json?address={number}+{urlStreet}+{urlCity}+{urlCountry}&key={ApiKey}").Result;
                return new Coordinates { GLat = geoCodingData.results[0].geometry.location.lat, GLong = geoCodingData.results[0].geometry.location.lng };
            }
            catch (Exception)
            {
                throw new Exception("Could not retrieve location coordinates for the address provided!");
            }
        }

        public int GeoDistance(Coordinates origin, Coordinates destination)
        {
            try
            {
                var geoDistanceData = PostCall<GeoDistanceData>($"https://maps.googleapis.com/maps/api/distancematrix/json?origins={origin.GLat},{origin.GLong}&destinations={destination.GLat},{destination.GLong}&key={ApiKey}").Result;
                return geoDistanceData.rows[0].elements[0].distance.value;
            }
            catch (Exception)
            {
                throw new Exception("Could not retrieve distance information from the coordinates provided!");
            }
        }

        public List<int> GeoDistances(Coordinates origin, List<Coordinates> destinations)
        {
            try
            {
                var destinationsList = string.Join("|", destinations.Select(d => $"{d.GLat},{d.GLong}"));
                var geoDistanceData = PostCall<GeoDistanceData>($"https://maps.googleapis.com/maps/api/distancematrix/json?origins={origin.GLat},{origin.GLong}&destinations={destinationsList}&key={ApiKey}").Result;
                return geoDistanceData.rows[0].elements.Select(e => e.distance.value).ToList();
            }
            catch (Exception)
            {
                throw new Exception("Could not retrieve distance information from the coordinates provided!");
            }
        }

        public Coordinates GeoLocation()
        {
            try
            {
                var geoLocationData = PostCall<GeoLocationData>($"https://www.googleapis.com/geolocation/v1/geolocate?key={ApiKey}").Result;
                return new Coordinates { GLat = geoLocationData.location.lat, GLong = geoLocationData.location.lng };
            }
            catch (Exception)
            {
                throw new Exception("Could not retrieve geo location!");
            }
        }
    }
}
