using BurgerBackend.Controllers;
using BurgerBackend.DTO;
using BurgerBackend.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace BurgerBackendTests
{
    class RestaurantsControllerTests
    {
        private RestaurantsController restaurantsController;
        private Mock<IGeoService> mockGeoService;

        [SetUp]
        public void Setup()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            mockGeoService = new Mock<IGeoService>();
            var mockImageSerce = new Mock<IImageSerce>();
            var repo = TestDb.CreateTestBurgerRepository(connection);
            restaurantsController = new RestaurantsController(repo, new Mappers(), mockGeoService.Object, mockImageSerce.Object);
        }

        [Test]
        public void GetAllRestaurants_should_return_restaurants_http_OK()
        {
            Assert.NotNull(restaurantsController);
            var restaurants = restaurantsController.GetAllRestaurants();
            Assert.NotNull(restaurants);
            restaurants.Result.Should().BeOfType<OkObjectResult>().Which.StatusCode.Should().Be((int)HttpStatusCode.OK);
            var result = restaurants.Result as OkObjectResult;
            var restaurantsResults = (result.Value as IEnumerable<Restaurant>).ToList();
            Assert.AreEqual(2, restaurantsResults.Count);
        }

        [Test]
        public void GetRestaurantByName_valid_input_should_return_restaurant_http_OK()
        {
            Assert.NotNull(restaurantsController);
            var restaurant = restaurantsController.GetRestaurantByName("Black Cab");
            Assert.NotNull(restaurant);
            restaurant.Result.Should().BeOfType<OkObjectResult>().Which.StatusCode.Should().Be((int)HttpStatusCode.OK);
            var result = restaurant.Result as OkObjectResult;
            var restaurantResult = result.Value as Restaurant;
            Assert.NotNull(restaurantResult);
            Assert.AreEqual("Black Cab", restaurantResult.Name);
        }


        [Test]
        public void CreateRestaurant_valid_input_should_create_restaurant_return_http_OK()
        {
            Assert.NotNull(restaurantsController);

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
                SundayClose = "22:00"
            };

            var restaurant = new Restaurant { Name = "Black Cab Test", Country = "Hungary", City = "Budapest", Street = "Mester", Number = 46, PostCode = "1095", Hours = hours };

            mockGeoService.Setup(x => x.GeoCoding(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>()))
                      .Returns(new Coordinates { GLat = 47.478623, GLong = 19.073896 });

            var createResult = restaurantsController.CreateRestaurant(restaurant);
            Assert.NotNull(createResult);
            createResult.Should().BeOfType<OkResult>().Which.StatusCode.Should().Be((int)HttpStatusCode.OK);

            var restaurantTest = restaurantsController.GetRestaurantByName("Black Cab Test");
            Assert.NotNull(restaurantTest);
            restaurantTest.Result.Should().BeOfType<OkObjectResult>().Which.StatusCode.Should().Be((int)HttpStatusCode.OK);
            var result = restaurantTest.Result as OkObjectResult;
            var restaurantResult = result.Value as Restaurant;
            Assert.NotNull(restaurantResult);
            Assert.AreEqual("Black Cab Test", restaurantResult.Name);

        }
    }
}
