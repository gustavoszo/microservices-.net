using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ItemService.Dtos;
using ItemService.Data;
using ItemService.Models;

namespace ItemService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ItemController : ControllerBase
{
    private readonly IItemRepository _repository;
    private readonly IMapper _mapper;

    public ItemController(IItemRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    [HttpGet]
    public ActionResult<IEnumerable<ItemReadDto>> GetItensByRestaurant(int restaurantId)
    {

        if (!_repository.RestaurantExists(restaurantId))
        {
            return NotFound();
        }

        var itens = _repository.GetItensByRestaurant(restaurantId);

        return Ok(_mapper.Map<IEnumerable<ItemReadDto>>(itens));
    }

    [HttpGet("{ItemId}", Name = "GetItemByRestaurant")]
    public ActionResult<ItemReadDto> GetItemByRestaurant(int restaurantId, int itemId)
    {
        if (!_repository.RestaurantExists(restaurantId))
        {
            return NotFound();
        }

        var item = _repository.GetItem(restaurantId, itemId);
        if (item == null) return NotFound();

        return Ok(_mapper.Map<ItemReadDto>(item));
    }

    [HttpPost]
    public ActionResult<ItemReadDto> CreateItemForRestaurant(int restaurantId, ItemCreateDto itemDto)
    {
        if (!_repository.RestaurantExists(restaurantId)) return NotFound();

        var item = _mapper.Map<Item>(itemDto);

        _repository.CreateItem(restaurantId, item);
        _repository.SaveChanges();

        var itemReadDto = _mapper.Map<ItemReadDto>(item);

        return CreatedAtRoute(nameof(GetItemByRestaurant),
            new { restaurantId, ItemId = itemReadDto.Id }, itemReadDto);
    }

}
