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
    class ReviewsControllerTests
    {
        private ReviewsController reviewsController;
        [SetUp]
        public void Setup()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            var repo = TestDb.CreateTestBurgerRepository(connection);
            reviewsController = new ReviewsController(repo, new Mappers());
        }

        [Test]
        public void GetReviewsForRestaurant_should_return_reviews_http_OK()
        {
            Assert.NotNull(reviewsController);
            var reviews = reviewsController.GetAllReviewsForRestaurant("Black Cab");
            Assert.NotNull(reviews);
            reviews.Result.Should().BeOfType<OkObjectResult>().Which.StatusCode.Should().Be((int)HttpStatusCode.OK);
            var result = reviews.Result as OkObjectResult;
            var reviewsResults = (result.Value as IEnumerable<Review>).ToList();
            Assert.AreEqual(1, reviewsResults.Count);
        }

        [Test]
        public void GetReviewsForRestaurant_invalid_restaurant_should_return_empty_http_OK()
        {
            Assert.NotNull(reviewsController);
            var reviews = reviewsController.GetAllReviewsForRestaurant("McDonalds");
            Assert.NotNull(reviews);
            reviews.Result.Should().BeOfType<OkObjectResult>().Which.StatusCode.Should().Be((int)HttpStatusCode.OK);
            var result = reviews.Result as OkObjectResult;
            var reviewsResults = (result.Value as IEnumerable<Review>).ToList();
            Assert.AreEqual(0, reviewsResults.Count);
        }

        [Test]
        public void CreateReviewForRestaurant_valid_restaurant_should_create_and_return_http_OK()
        {
            Assert.NotNull(reviewsController);
            var review = new Review { Taste = 5, Texture = 3, VisualRepresentation = 3 };
            var reviews = reviewsController.CreateReviewForRestaurant("Black Cab", review);
            Assert.NotNull(reviews);
            reviews.Should().BeOfType<OkResult>().Which.StatusCode.Should().Be((int)HttpStatusCode.OK);

            var reviewsUpdated = reviewsController.GetAllReviewsForRestaurant("Black Cab");
            Assert.NotNull(reviews);
            reviewsUpdated.Result.Should().BeOfType<OkObjectResult>().Which.StatusCode.Should().Be((int)HttpStatusCode.OK);
            var result = reviewsUpdated.Result as OkObjectResult;
            var reviewsResults = (result.Value as IEnumerable<Review>).ToList();
            Assert.AreEqual(2, reviewsResults.Count);
        }

        [Test]
        public void CreateReviewForRestaurant_ivalid_restaurant_should_return_http_BadRequest()
        {
            Assert.NotNull(reviewsController);
            var review = new Review { Taste = 5, Texture = 3, VisualRepresentation = 3 };
            var reviews = reviewsController.CreateReviewForRestaurant("McDonalds", review);
            Assert.NotNull(reviews);
            reviews.Should().BeOfType<BadRequestObjectResult>().Which.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
        }
    }
}
