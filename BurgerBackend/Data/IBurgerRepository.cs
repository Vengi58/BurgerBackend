using BurgerBackend.Models;
using System.Collections.Generic;

namespace BurgerBackend.Data
{
    public interface IBurgerRepository
    {
        List<Restaurant> GetRestaurants();
        void CreateRestaurant(Restaurant restaurant);
        Restaurant FindRestaurantByName(string name);
        List<Review> GetReviews();
        List<Review> GetReviewsForRestaurant(string restaurantName);
        void CreateReviewForRestaurant(Review review);
        void UpdateRestaurant(Restaurant restaurant);
        void DeleteRestaurant(Restaurant restaurant);
    }
}
