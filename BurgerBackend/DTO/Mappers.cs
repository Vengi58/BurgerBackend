using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BurgerBackend.DTO
{
    public interface IMappers
    {
        public Restaurant ToDTO(Models.Restaurant restaurant);
        public Models.Restaurant ToModel(Restaurant restaurant);
        public Review ToDTO(Models.Review restaurant);
        public Models.Review ToModel(string restaurantName, Review review);
        public Hours ToDTO(Models.Hours hours);
        public Models.Hours ToModel(Hours hours);
    }

    public class Mappers: IMappers
    {

        IMapper iMapper;
        public Mappers()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Models.Restaurant, Restaurant>();
                cfg.CreateMap<Restaurant, Models.Restaurant>();
                cfg.CreateMap<Models.Review, Review>();
                cfg.CreateMap<Review, Models.Review>();
                cfg.CreateMap<Models.Hours, Hours>();
                cfg.CreateMap<Hours, Models.Hours>();
            });

            iMapper = config.CreateMapper();
        }

        public Restaurant ToDTO(Models.Restaurant restaurant)
        {
            return iMapper.Map<Models.Restaurant, Restaurant>(restaurant);
        }

        public Review ToDTO(Models.Review review)
        {
            return iMapper.Map<Models.Review, Review>(review);
        }

        public Hours ToDTO(Models.Hours hours)
        {
            return iMapper.Map<Models.Hours, Hours>(hours);
        }

        public Models.Restaurant ToModel(Restaurant restaurant)
        {
            return iMapper.Map<Restaurant, Models.Restaurant>(restaurant);
        }

        public Models.Review ToModel(string restaurantName, Review review)
        {
            var mappedReview = iMapper.Map<Review, Models.Review>(review);
            mappedReview.RestaurantName = restaurantName;
            return mappedReview;
        }

        public Models.Hours ToModel(Hours hours)
        {
            return iMapper.Map<Hours, Models.Hours>(hours);
        }
    }
}
