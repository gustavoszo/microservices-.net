using RestauranteService.Dtos;

namespace RestauranteService.Clients
{
    public interface IItemServiceHttpClient
    {
        public Task EnviaRestauranteParaItemServiceAsync(RestauranteReadDto readDto);
    }
}
