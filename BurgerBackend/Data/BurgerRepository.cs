using BurgerBackend.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

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
            DBContext.Restaurants.Add(restaurant);
            DBContext.SaveChanges();
        }

        public void CreateReviewForRestaurant(Review review)
        {
            DBContext.Reviews.Add(review);
            DBContext.SaveChanges();
        }

        public void DeleteRestaurant(Restaurant restaurant)
        {
            DBContext.Restaurants.Remove(restaurant);
            DBContext.SaveChanges();
        }

        public Restaurant FindRestaurantByName(string name)
        {
            return DBContext.Restaurants.Include(r => r.Hours).FirstOrDefault(r => name.Equals(r.Name));
        }

        public List<Restaurant> GetRestaurants()
        {
            return DBContext.Restaurants.Include(r => r.Hours).ToList();
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
            var reviews = DBContext.Reviews.Where(r => r.RestaurantName == restaurant.Name);
            return reviews != null ? reviews.ToList() : new List<Review>();
        }

        public void UpdateRestaurant(Restaurant restaurant)
        {
            var restaurantFound = FindRestaurantByName(restaurant.Name);
            restaurantFound.City = restaurant.City;
            restaurantFound.Country = restaurant.Country;
            restaurantFound.Hours = restaurant.Hours;
            restaurantFound.Number = restaurant.Number;
            restaurantFound.PostCode = restaurant.PostCode;
            restaurantFound.Street = restaurant.Street;
            DBContext.SaveChanges();
        }
    }
}
