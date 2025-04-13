using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RestauranteService.Clients;
using RestauranteService.Data;
using RestauranteService.Dtos;
using RestauranteService.Models;
using RestauranteService.Producers;

namespace RestauranteService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RestaurantController : ControllerBase
{
    private readonly IRestaurantRepository _restaurantRepository;
    private readonly IMapper _mapper;
    private readonly RestaurantProducer _restaurantProducer;
    private readonly ILogger<RestaurantController> _logger;

    public RestaurantController(IRestaurantRepository repository, IMapper mapper, RestaurantProducer restaurantProducer, ILogger<RestaurantController> logger)
    {
        _restaurantRepository = repository;
        _mapper = mapper;
        _restaurantProducer = restaurantProducer;
        _logger = logger;   
    }

    [HttpGet]
    public ActionResult<IEnumerable<RestaurantReadDto>> GetAllRestaurantes()
    {

        var restaurants = _restaurantRepository.GetAllRestaurants();

        return Ok(_mapper.Map<IEnumerable<RestaurantReadDto>>(restaurants));
    }

    [HttpGet("{id}", Name = "GetRestaurantById")]
    public ActionResult<RestaurantReadDto> GetRestaurantById(int id)
    {
        var restaurant = _restaurantRepository.GetRestaurantById(id);
        if (restaurant != null)
        {
            return Ok(_mapper.Map<RestaurantReadDto>(restaurant));
        }

        return NotFound();
    }

    [HttpPost]
    public async Task<ActionResult<RestaurantReadDto>> CreateRestaurant(RestaurantCreateDto restaurantCreateDto)
    {
        var restaurant = _mapper.Map<Restaurant>(restaurantCreateDto);
        _restaurantRepository.CreateRestaurant(restaurant);
        _restaurantRepository.SaveChanges();

        var restaurantReadDto = _mapper.Map<RestaurantReadDto>(restaurant);
        await _restaurantProducer.SendRestaurant(restaurantReadDto);

        return CreatedAtRoute(nameof(GetRestaurantById), new { restaurantReadDto.Id }, restaurantReadDto);
    }
}
