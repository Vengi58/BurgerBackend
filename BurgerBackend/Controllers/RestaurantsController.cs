﻿using BurgerBackend.Data;
using BurgerBackend.DTO;
using BurgerBackend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Web.Helpers;
using System;
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
        private readonly IBurgerRepository Repository;
        private readonly IMappers Mappers;
        private readonly IGeoService GeoHelper;
        private readonly IImageSerce ImageService;
        public RestaurantsController(IBurgerRepository repository, IMappers mappers, IGeoService geoHelper, IImageSerce imageSerce)
        {
            Repository = repository;
            Mappers = mappers;
            GeoHelper = geoHelper;
            ImageService = imageSerce;
        }

        [HttpGet("location")]
        public ActionResult<Coordinates> GetLocation()
        {
            var location = GeoHelper.GeoLocation();
            var c = GeoHelper.GeoDistances(
                location,
                new List<Coordinates> {
                    new Coordinates{ GLat = 47.4984183, GLong = 19.0566239 },
                    new Coordinates { GLat = 47.510417, GLong = 18.925901 }
                    });
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
        public ActionResult CreateRestaurant([FromBody] DTO.Restaurant restaurant)
        {
            try
            {
                if (restaurant == null) throw new ArgumentException("Restaurant must be provided!");
                var c = GeoHelper.GeoCoding(restaurant.Country, restaurant.City, restaurant.Street, restaurant.Number, restaurant.PostCode);
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
        public ActionResult UpdateRestaurant([FromBody] DTO.Restaurant restaurant)
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

        [HttpGet("{maxDistance}/{maxRecommendation}/{minimumAvgScore}")]
        public ActionResult<List<string>> GetRecommendations(int maxDistance, int maxRecommendation, int minimumAvgScore)
        {
            try
            {                
                return Ok(Repository.GetRecommendations(maxDistance, maxRecommendation, minimumAvgScore));
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
