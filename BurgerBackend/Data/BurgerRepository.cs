using BurgerBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BurgerBackend.Data
{
    public class BurgerRepository: IBurgerRepository
    {
        BurgerDBContext DBContext;
        public BurgerRepository(BurgerDBContext dBContext)
        {
            DBContext = dBContext;
        }

        public void CreateRestaurant(Restaurant restaurant)
        {
            var result = DBContext.Restaurants.Add(restaurant);
            DBContext.SaveChanges();
        }

        public Restaurant FindRestaurantByName(string name)
        {
            return DBContext.Restaurants.FirstOrDefault(r => name.Equals(r.Name));
        }

        public List<Restaurant> GetRestaurants()
        {
            return DBContext.Restaurants.ToList();
        }

        public List<Review> GetReviews()
        {
            return DBContext.Reviews.ToList();
        }

        public List<Review> GetReviewsForRestaurant(string restaurantName)
        {
            var result = new List<Review>();
            var restaurant = FindRestaurantByName(restaurantName);
            if (restaurant == null) return result;
            var reviews = DBContext.Reviews.Where(r => r.RestaurantID == restaurant.RestaurantID);
            return reviews != null ? reviews.ToList() : new List<Review>();
        }
    }
}
