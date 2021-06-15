using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BurgerBackend.Services
{
    public interface IGeoHelper
    {
        Task<GeoInfo> GetGeoInfo();
    }
    public class GeoHelper: IGeoHelper
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly HttpClient _httpClient;

        public GeoHelper(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _httpClient = new HttpClient()
            {
                Timeout = TimeSpan.FromSeconds(5)
            };
        }

        public async Task<GeoInfo> GetGeoInfo()
        {
            //var ip = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress;
            try
            {
             //   var res = await _httpClient.PostAsync($"https://www.googleapis.com/geolocation/v1/geolocate?key=AIzaSyDG3jEVhnHN-im18GtgGPgH-XZhp9M6Kwg");
             /*
                if (res.IsSuccessStatusCode)
                {
                    var json = await res.Content.ReadAsStringAsync();

                    return JsonConvert.DeserializeObject<GeoInfo>(json);
                }
                var response = await _httpClient.GetAsync($"http://api.ipstack.com/{ip}?access_key={accesKey}");

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();

                    return JsonConvert.DeserializeObject<GeoInfo>(json);
                }
             */
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, "Failed to retrieve geo info for ip '{0}'", ip);
            }

            return null;
        }
    }
}
