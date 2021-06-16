using System;
using System.ComponentModel.DataAnnotations;

namespace BurgerBackend.DTO
{
    public class Restaurant
    {
        [Required]
        [MinLength(3)]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required]
        [MinLength(2)]
        [MaxLength(100)]
        public String Country { get; set; }
        [Required]
        [MinLength(2)]
        [MaxLength(100)]
        public String City { get; set; }
        [Required]
        [MinLength(2)]
        [MaxLength(100)]
        public String Street { get; set; }
        [Required]
        [Range(1,10000)]
        public int Number { get; set; }
        [Required]
        [Range(1, 10000)]
        public String PostCode { get; set; }
        [Required]
        public Hours Hours { get; set; }
    }

    public class RestaurantWithDistance: Restaurant
    {
        public RestaurantWithDistance(Restaurant restaurant, int distance)
        {
            City = restaurant.City;
            Country = restaurant.Country;
            Hours = restaurant.Hours;
            Name = restaurant.Name;
            Number = restaurant.Number;
            PostCode = restaurant.PostCode;
            Street = restaurant.Street;
            Distance = distance;
        }
        public int Distance { get; set; }
    }
}
