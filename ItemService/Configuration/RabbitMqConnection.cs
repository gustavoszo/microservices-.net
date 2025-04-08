using RabbitMQ.Client;

namespace RestauranteService.Configuration
{
    // Estou usando o RabbitMqConnection dentro de um serviço injetado como Singleton, o .NET chamará automaticamente o DisposeAsync() no shutdown da aplicação (pq ele está registrado no DI).
    public class RabbitMqConnection : IAsyncDisposable
    {
        public IConnection Connection { get; private set; }
        public IChannel Channel { get; private set; }

        private readonly string _exchange;
        private readonly string _queue;    
        private readonly IConfiguration _configuration;

        public RabbitMqConnection(IConfiguration configuration)
        {
            _configuration = configuration;
            _exchange = _configuration["broker.exchange.restaurant.name"];
            _queue = _configuration["broker.queue.item.name"];
        }

        public async Task InitializeAsync()
        {
            var factory = new ConnectionFactory
            {
                HostName = _configuration["RabbitMQHost"],
                Port = Int32.Parse(_configuration["RabbitMQPort"]),
            };

            Connection = await factory.CreateConnectionAsync();
            Channel = await Connection.CreateChannelAsync();

            await Channel.ExchangeDeclareAsync(
                exchange: _exchange,
                type: ExchangeType.Fanout,
                durable: true,
                autoDelete: false,
                arguments: null
            );

            await Channel.QueueDeclareAsync(
                queue: _queue,
                durable: true,
                exclusive: false,
                autoDelete: false
            );

            await Channel.QueueBindAsync(
                queue: _queue,
                exchange: _exchange,
                routingKey: ""
            );
        }

        public async ValueTask DisposeAsync()
        {
            if (Channel != null)
            {
                await Channel.CloseAsync();
                await Channel.DisposeAsync();
            }

            if (Connection != null)
            {
                await Connection.CloseAsync();
                await Connection.DisposeAsync();
            }
        }
    }
}
