using BurgerBackend.Data;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Microsoft.Data.Sqlite;
using BurgerBackend.Models;

namespace BurgerBackendTests
{
    public class BurgerRepositoryTests
    {
        private SqliteConnection connection;
        private BurgerRepository repo;

        [SetUp]
        public void Setup()
        {
            connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            repo = TestDb.CreateTestBurgerRepository(connection);
        }

        [TearDown]
        public void TearDown()
        {
            connection.Close();
        }

        [Test]
        public void GetRestaurant_should_return_setup_data()
        {
            var restaurants = repo.GetRestaurants();

            Assert.NotNull(restaurants);
            Assert.AreEqual(2, restaurants.Count);
        }

        [Test]
        public void CreateRestaurant_valid_input_should_pass()
        {
            var hours = new Hours
            {
                MondayOpen = "10:00",
                MondayClose = "22:00",
                TuesdayOpen = "10:00",
                TuesdayClose = "22:00",
                WednesdayOpen = "10:00",
                WednesdayClose = "22:00",
                ThursdayOpen = "10:00",
                ThursdayClose = "22:00",
                FridayOpen = "10:00",
                FridayClose = "22:00",
                SaturdayOpen = "10:00",
                SaturdayClose = "22:00",
                SundayOpen = "10:00",
                SundayClose = "22:00",
                RestaurantName = "Deep Burger"
            };

            var restaurant = new Restaurant { Name = "Deep Burger", Country = "Hungary", City = "Budapest", Street = "Erzsebet krt", Number = 19, PostCode = "1073", GLat = 47.46694369999999, GLong = 19.3009634, Hours = hours };
            repo.CreateRestaurant(restaurant);
            var restaurants = repo.GetRestaurants();

            Assert.NotNull(restaurants);
            Assert.IsTrue(restaurants.Contains(restaurant));
        }

        [Test]
        public void DeleteRestaurant_existing_restaurant_should_pass()
        {
            var restaurants = repo.GetRestaurants();
            var restaurant = restaurants[0];

            repo.DeleteRestaurant(restaurant);

            restaurants = repo.GetRestaurants();

            Assert.True(!restaurants.Contains(restaurant));
        }

        [Test]
        public void FindRestaurantByName_valid_input_should_pass()
        {
            var restaurant = repo.FindRestaurantByName("Black Cab");

            Assert.NotNull(restaurant);
        }

        [Test]
        public void FindRestaurantByName_invalid_input_should_return_null()
        {
            var restaurant = repo.FindRestaurantByName("McDonalds");

            Assert.Null(restaurant);
        }

        [Test]
        public void GetReviewsForRestaurant_valid_input_should_return_reviews()
        {
            var reviews = repo.GetReviewsForRestaurant("Black Cab");

            Assert.NotNull(reviews);
            Assert.AreEqual(1, reviews.Count);
        }

        [Test]
        public void GetReviewsForRestaurant_invalid_input_should_return_empty_reviews()
        {
            var reviews = repo.GetReviewsForRestaurant("McDonalds");

            Assert.NotNull(reviews);
            Assert.AreEqual(0, reviews.Count);
        }

        [Test]
        public void UpdateRestaurant_valid_input_should_update()
        {
            var restaurant = repo.FindRestaurantByName("Black Cab");
            restaurant.Number = 10;
            repo.UpdateRestaurant(restaurant);
            var updatedRestaurant = repo.FindRestaurantByName("Black Cab");

            Assert.AreEqual(10, updatedRestaurant.Number);
        }

        [Test]
        public void CreateReviewForRestaurant_valid_input_should_add_review_to_restaurant()
        {
            var reviews = repo.GetReviewsForRestaurant("Black Cab");

            var review = new Review { RestaurantName = "Black Cab", Taste = 3, Texture = 4, VisualRepresentation = 3 };
            repo.CreateReviewForRestaurant(review);
            var updatedReviews = repo.GetReviewsForRestaurant("Black Cab");

            Assert.AreEqual(reviews.Count + 1, updatedReviews.Count);
            Assert.True(updatedReviews.Contains(review));
        }

        [Test]
        public void CreateReviewForRestaurant_invalid_restaurant_name_should_throw_DbUpdateException()
        {
            var reviews = repo.GetReviewsForRestaurant("Black Cab");

            var review = new Review { RestaurantName = "Black Cabb", Taste = 3, Texture = 4, VisualRepresentation = 3 };
            Assert.Throws<DbUpdateException>(() => repo.CreateReviewForRestaurant(review));
        }
    }
}