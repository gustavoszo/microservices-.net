using System;
using RestauranteService.Models;

namespace RestauranteService.Data
{
    public class RestaurantRepository : IRestaurantRepository
    {
        private readonly AppDbContext _context;

        public RestaurantRepository(AppDbContext context)
        {
            _context = context;
        }

        public void CreateRestaurant(Restaurant restaurant)
        {
            if (restaurant == null)
            {
                throw new ArgumentNullException(nameof(restaurant));
            }

            _context.Restaurants.Add(restaurant);
        }
        public IEnumerable<Restaurant> GetAllRestaurants()
        {
            return _context.Restaurants.ToList();
        }

        public Restaurant GetRestaurantById(int id) => _context.Restaurants.FirstOrDefault(c => c.Id == id);

        public void SaveChanges()

        {
            _context.SaveChanges();
        }
    }
}