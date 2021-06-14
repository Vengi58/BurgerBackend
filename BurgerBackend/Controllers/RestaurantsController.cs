using BurgerBackend.Data;
using BurgerBackend.DTO;
using BurgerBackend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Web.Helpers;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BurgerBackend.Models
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantsController : ControllerBase
    {
        private IBurgerRepository Repository;
        private IMappers Mappers;
        private IGeoHelper GeoHelper;
        public RestaurantsController(IBurgerRepository repository, IMappers mappers, IGeoHelper geoHelper)
        {
            Repository = repository;
            Mappers = mappers;
            GeoHelper = geoHelper;
        }

        [HttpGet("location")]
        public ActionResult<GeoInfo> GetLocation()
        {
            var location = GeoHelper.GetGeoInfo().Result;
            return Ok(location);
        }

        [HttpGet]
        public ActionResult<IEnumerable<DTO.Restaurant>> GetAllRestaurants()
        {
            var restaurants = Repository.GetRestaurants().Select(r => Mappers.ToDTO(r));
            return Ok(restaurants);
        }

        [HttpGet("{name}")]
        public ActionResult<DTO.Restaurant> GetRestaurantByName(string name)
        {
            try
            {
                var restaurant = Repository.FindRestaurantByName(name);
                return restaurant != null
                    ? Ok(Mappers.ToDTO(Repository.FindRestaurantByName(name)))
                    : NotFound(name);
            }
            catch (System.Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public ActionResult CreateRestaurant([FromBody] DTO.Restaurant restaurant)
        {
            Repository.CreateRestaurant(Mappers.ToModel(restaurant));
            return Ok();
        }

        [HttpPut("{id}")]
        public void UpdateRestaurant(int id, [FromBody] string value)
        {
        }

        [HttpDelete("{name}")]
        public void DeleteRestaurant(string name)
        {
        }

        [HttpPost("image")]
        public async Task<ActionResult> PostImage([FromForm] FileUpload fileUpload)
        {
            var fn = fileUpload.Files.FileName;

            return Ok();
            //var folderPath = "";
            //using (var fileContentStream = new System.IO.MemoryStream())
            //{
            //    await myFile.CopyToAsync(fileContentStream);
            //    await System.IO.File.WriteAllBytesAsync(Path.Combine(folderPath, myFile.FileName), fileContentStream.ToArray());
            //}
            //return CreatedAtRoute(routeName: "myFile", routeValues: new { filename = myFile.FileName }, value: null);
        }

    }
    public class FileUpload
    {
        public IFormFile Files { get; set; }
    }
}
