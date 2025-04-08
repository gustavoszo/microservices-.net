using AutoMapper;
using RestauranteService.Dtos;
using RestauranteService.Models;

namespace RestauranteService.Profiles
{
    public class RestauranteProfile : Profile
    {
        public RestauranteProfile()
        {
            CreateMap<Restaurant, RestaurantReadDto>();
            CreateMap<RestaurantCreateDto, Restaurant>();
        }
    }
}