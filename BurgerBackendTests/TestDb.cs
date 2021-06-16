using BurgerBackend.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.Sqlite;
using BurgerBackend.Models;

namespace BurgerBackendTests
{
    public class TestDb
    {
        public static BurgerRepository CreateTestBurgerRepository(SqliteConnection connection)
        {
            var options = new DbContextOptionsBuilder<BurgerDBContext>()
                .UseSqlite(connection)
                .Options;

            var context = new BurgerDBContext(options);
            context.Database.EnsureCreated();

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
                RestaurantName = "Black Cab"
            };
            var hours2 = new Hours
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
                RestaurantName = "Burger Market"
            };
            var review = new Review { RestaurantName = "Black Cab", Taste = 5, Texture = 4, VisualRepresentation = 4 };

            var restaurant = new Restaurant { Name = "Black Cab", Country = "Hungary", City = "Budapest", Street = "Mester", Number = 46, PostCode = "1095", GLat = 47.478623, GLong = 19.073896, Hours = hours };
            context.Restaurants.Add(restaurant);
            context.SaveChanges();
            restaurant = new Restaurant { Name = "Burger Market", Country = "Hungary", City = "Budapest", Street = "Kiraly utca", Number = 8, PostCode = "1061", GLat = 47.4984183, GLong = 19.0566239, Hours = hours2 };
            context.Restaurants.Add(restaurant);
            context.SaveChanges();
            context.Reviews.Add(review);
            context.SaveChanges();
            return new BurgerRepository(context);
        }
    }
}
