using System.Collections.Generic;
using ItemService.Models;

namespace ItemService.Data;

public interface IItemRepository
{
    void SaveChanges();

    IEnumerable<Restaurant> GetAllRestaurants();
    void CreateRestaurant(Restaurant restaurant);
    bool RestaurantExists(int restaurantId);
    bool ExternalRestaurantExists(int restauranteIdExterno);

    IEnumerable<Item> GetItensByRestaurant(int restaurantId);
    Item GetItem(int restaurantId, int itemId);
    void CreateItem(int restaurantId, Item item);
}
