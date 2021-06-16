using BurgerBackend.Data;
using BurgerBackend.DTO;
using BurgerBackend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BurgerBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantsController : ControllerBase
    {
        private readonly IBurgerRepository Repository;
        private readonly IMappers Mappers;
        private readonly IGeoService GeoService;
        private readonly IImageSerce ImageService;
        public RestaurantsController(IBurgerRepository repository, IMappers mappers, IGeoService geoService, IImageSerce imageSerce)
        {
            Repository = repository;
            Mappers = mappers;
            GeoService = geoService;
            ImageService = imageSerce;
        }

        [HttpGet("/location")]
        public ActionResult<Coordinates> GetMyLocation()
        {
            try
            {
                var location = GeoService.GeoLocation();
                return Ok(location);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet]
        public ActionResult<IEnumerable<Restaurant>> GetAllRestaurants()
        {
            try
            {
                var restaurants = Repository.GetRestaurants().Select(r => Mappers.ToDTO(r));
                return Ok(restaurants);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{name}")]
        public ActionResult<Restaurant> GetRestaurantByName(string name)
        {
            try
            {
                if (string.IsNullOrEmpty(name)) throw new ArgumentException("Restaurant name must be provided!");

                var restaurant = Repository.FindRestaurantByName(name);
                return restaurant != null
                    ? Ok(Mappers.ToDTO(Repository.FindRestaurantByName(name)))
                    : NotFound(name);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public ActionResult CreateRestaurant([FromBody] Restaurant restaurant)
        {
            try
            {
                if (restaurant == null) throw new ArgumentException("Restaurant must be provided!");
                var c = GeoService.GeoCoding(restaurant.Country, restaurant.City, restaurant.Street, restaurant.Number, restaurant.PostCode);
                var restaurantModel = Mappers.ToModel(restaurant);
                restaurantModel.GLat = c.GLat;
                restaurantModel.GLong = c.GLong;
                Repository.CreateRestaurant(restaurantModel);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        public ActionResult UpdateRestaurant([FromBody] Restaurant restaurant)
        {
            try
            {
                if (restaurant == null) throw new ArgumentException("Restaurant must be provided!");
                if (string.IsNullOrEmpty(restaurant.Name)) throw new ArgumentException("Restaurant name must be provided!");

                if (Repository.FindRestaurantByName(restaurant.Name) == null) return NotFound(restaurant.Name);

                Repository.UpdateRestaurant(Mappers.ToModel(restaurant));
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{name}")]
        public ActionResult DeleteRestaurant(string name)
        {
            try
            {
                if (string.IsNullOrEmpty(name)) throw new ArgumentException("Restaurant name must be provided!");

                var restaurant = Repository.FindRestaurantByName(name);
                if (restaurant == null) return NotFound(name);

                Repository.DeleteRestaurant(restaurant);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("Images/{restaurant}/{imageID}")]
        public ActionResult GetImageOfRestaurant(string restaurant, string imageID)
        {
            try
            {
                return File(ImageService.GetImageOfRestaurant(restaurant, imageID).ToArray(), "image/jpg", imageID);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("Images/{restaurant}")]
        public ActionResult<List<string>> GetImages(string restaurant)
        {
            try
            {
                return Ok(ImageService.GetImageIDs(restaurant));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("Images/{restaurant}")]
        public ActionResult PostImage(string restaurant, [FromForm] FileUpload fileUpload)
        {
            try
            {
                ImageService.UploadImage(restaurant, fileUpload.File);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{maxDistance}/{maxRecommendation}")]
        public ActionResult<List<RestaurantWithDistance>> GetRecommendations(int maxDistance, int maxRecommendation)
        {
            try
            {
                var restaurants = Repository.GetRestaurants();
                var myLocation = GeoService.GeoLocation();
                var distances = GeoService.GeoDistances(myLocation, restaurants.Select(r => new Coordinates { GLat = r.GLat, GLong = r.GLong }).ToList());
                var result = new List<RestaurantWithDistance>();
                for (int i = 0; i < distances.Count; i++)
                {
                    if(distances[i] <= maxDistance)
                    {
                        result.Add(new RestaurantWithDistance(Mappers.ToDTO(restaurants[i]), distances[i]));
                    }
                }
                return Ok(result.OrderBy(r => r.Distance).Take(maxRecommendation).ToList());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("{maxDistance}/{maxRecommendation}")]
        public ActionResult<List<RestaurantWithDistance>> GetRecommendationsForLocation(int maxDistance, int maxRecommendation, [FromBody] Coordinates coordinates)
        {
            try
            {
                var restaurants = Repository.GetRestaurants();
                var distances = GeoService.GeoDistances(coordinates, restaurants.Select(r => new Coordinates { GLat = r.GLat, GLong = r.GLong }).ToList());
                var result = new List<RestaurantWithDistance>();
                for (int i = 0; i < distances.Count; i++)
                {
                    if (distances[i] <= maxDistance)
                    {
                        result.Add(new RestaurantWithDistance(Mappers.ToDTO(restaurants[i]), distances[i]));
                    }
                }
                return Ok(result.OrderBy(r => r.Distance).Take(maxRecommendation).ToList());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
    public class FileUpload
    {
        public IFormFile File { get; set; }
    }
}
