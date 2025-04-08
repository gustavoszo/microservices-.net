using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ItemService.Data;
using ItemService.Dtos;

namespace ItemService.Controllers;

[Route("api/item/[controller]")]
[ApiController]
public class RestaurantController : ControllerBase
{
    private readonly IItemRepository _repository;
    private readonly IMapper _mapper;

    public RestaurantController(IItemRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    [HttpGet]
    public ActionResult<IEnumerable<RestaurantReadDto>> GetRestaurants()
    {
        var restaurants = _repository.GetAllRestaurants();

        return Ok(_mapper.Map<IEnumerable<RestaurantReadDto>>(restaurants));
    }

    [HttpPost]
    public ActionResult ReceiveRestaurantFromRestaurantService(RestaurantReadDto dto)
    {
        Console.WriteLine(dto.Id);
        return Ok();
    }

    [HttpGet("teste")]
    public ActionResult Test()
    {
        Console.WriteLine("Requisição bem sucedida");
        return Ok("OK");
    }

}
