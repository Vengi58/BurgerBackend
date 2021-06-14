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
        private IBurgerRepository Repository;
        private IMappers Mappers;
        public ReviewsController(IBurgerRepository repository, IMappers mappers)
        {
            Repository = repository;
            Mappers = mappers;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Review>> GetAllReviews()
        {
            return Ok(Repository.GetReviews().Select(r => Mappers.ToDTO(r)));
        }

        [HttpGet("{restaurantName}")]
        public ActionResult<IEnumerable<Review>> GetAllReviewsForRestaurant(string restaurantName)
        {
            return Ok(Repository.GetReviewsForRestaurant(restaurantName).Select(r => Mappers.ToDTO(r)));
        }

        [HttpPost("{restaurantName}")]
        public void CreateReviewForRestaurant(string restaurantName, [FromBody] Review value)
        {
        }
    }
}
