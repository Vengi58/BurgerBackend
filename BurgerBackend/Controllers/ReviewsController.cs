using BurgerBackend.Data;
using BurgerBackend.DTO;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BurgerBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly IBurgerRepository Repository;
        private readonly IMappers Mappers;

        public ReviewsController(IBurgerRepository repository, IMappers mappers)
        {
            Repository = repository;
            Mappers = mappers;
        }

        [HttpGet("{restaurantName}")]
        public ActionResult<IEnumerable<Review>> GetAllReviewsForRestaurant(string restaurantName)
        {
            try
            {
                if (string.IsNullOrEmpty(restaurantName)) throw new ArgumentException("Restaurant name must be provided!");

                return Ok(Repository.GetReviewsForRestaurant(restaurantName).Select(r => Mappers.ToDTO(r)));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("{restaurantName}")]
        public ActionResult CreateReviewForRestaurant(string restaurantName, [FromBody] Review review)
        {
            try
            {
                if (string.IsNullOrEmpty(restaurantName)) throw new ArgumentException("Restaurant name must be provided!");
                if (review == null) throw new ArgumentException("Review must be provided!");

                var restaurant = Repository.FindRestaurantByName(restaurantName);
                if (restaurant == null) throw new ArgumentException($"Restaurant \"{restaurantName}\" not found!");

                Repository.CreateReviewForRestaurant(Mappers.ToModel(restaurant.Name, review));
                return Ok();

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
