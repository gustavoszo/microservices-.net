using RestauranteService.Models;

namespace RestauranteService.Data;

public interface IRestaurantRepository
{
    void SaveChanges();

    IEnumerable<Restaurant> GetAllRestaurants();
    Restaurant GetRestaurantById(int id);
    void CreateRestaurant(Restaurant restaurant);
}
