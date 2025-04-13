using RabbitMQ.Client;

namespace RestauranteService.Configuration;

public class RabbitMqConnection : IAsyncDisposable
{
    public IConnection Connection { get; private set; }
    public IChannel Channel { get; private set; }

    private readonly string _exchange;
    private readonly IConfiguration _configuration;

    public RabbitMqConnection(IConfiguration configuration)
    {
        _configuration = configuration;
        _exchange = _configuration["broker.exchange.restaurant.name"];
    }

    public async Task InitializeAsync()
    {
        var factory = new ConnectionFactory
        {
            HostName = _configuration["RabbitMQHost"],
            Port = Int32.Parse(_configuration["RabbitMQPort"]),
            UserName = _configuration["RabbitMQUser"],
            Password = _configuration["RabbitMQPassword"]
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
