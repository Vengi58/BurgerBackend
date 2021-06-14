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
    }

    public class Mappers: IMappers
    {

        IMapper iMapper;
        public Mappers()
        {
            MapperConfiguration config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Models.Restaurant, Restaurant>();
                cfg.CreateMap<Models.Review, Review>();
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

        public Models.Restaurant ToModel(Restaurant restaurant)
        {
            return iMapper.Map<Restaurant, Models.Restaurant>(restaurant);
        }
    }
}
