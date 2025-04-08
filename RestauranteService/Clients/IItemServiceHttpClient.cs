using RestauranteService.Dtos;

namespace RestauranteService.Clients
{
    public interface IItemServiceHttpClient
    {
        public Task SendRestaurantToItemServiceAsync(RestaurantReadDto readDto);
    }
}
