using RabbitMQ.Client;
using RestauranteService.Configuration;
using RestauranteService.Dtos;
using System.Text;
using System.Text.Json;

namespace RestauranteService.Producers
{
    public class RestaurantProducer
    {
        private readonly RabbitMqConnection _rabbitMQ;
        private readonly IConfiguration _configuration;
        private readonly string _exchange;

        public RestaurantProducer(RabbitMqConnection rabbit, IConfiguration configuration)
        {
            _rabbitMQ = rabbit;
            _configuration = configuration;
            _exchange = _configuration["broker.exchange.restauarant.name"];
        }

        public async Task SendRestaurant(RestaurantReadDto restauranteReadDto)
        {
            var message = JsonSerializer.Serialize(restauranteReadDto);
            var body = Encoding.UTF8.GetBytes(message);
            await _rabbitMQ.Channel.BasicPublishAsync(exchange: _exchange, routingKey: "", body: body);
        }
    }
}
