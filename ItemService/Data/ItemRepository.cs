using System;
using ItemService.Models;

namespace ItemService.Data
{
    public class ItemRepository : IItemRepository
    {
        private readonly AppDbContext _context;

        public ItemRepository(AppDbContext context)
        {
            _context = context;
        }

        public void CreateItem(int restaurantId, Item item)
        {
            item.IdRestaurant = restaurantId;
            _context.Itens.Add(item);
        }

        public void CreateRestaurant(Restaurant restaurant)
        {
            _context.Restaurants.Add(restaurant);
        }
        public bool RestaurantExists(int restaurantId)
        {
            return _context.Restaurants.Any(restaurante => restaurante.Id == restaurantId);
        }

        public IEnumerable<Restaurant> GetAllRestaurants()
        {
            return _context.Restaurants.ToList();
        }

        public Item GetItem(int restaurantId, int itemId) => _context.Itens
            .Where(item => item.IdRestaurant == restaurantId && item.Id == itemId).FirstOrDefault();

        public IEnumerable<Item> GetItensByRestaurant(int restaurantId)
        {
            return _context.Itens
                .Where(item => item.IdRestaurant == restaurantId);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}