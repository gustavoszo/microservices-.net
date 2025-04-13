namespace RestauranteService.Configuration
{
    public class RabbitMqInitializer : IHostedService
    {
        private readonly RabbitMqConnection _rabbitMqConnection;
        private readonly ILogger<RabbitMqConnection> _logger;

        public RabbitMqInitializer(RabbitMqConnection rabbitMqConnection, ILogger<RabbitMqConnection> logger)
        {
            _rabbitMqConnection = rabbitMqConnection;
            _logger = logger;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            const int maxRetries = 20;
            int delaySeconds = 5;

            for (int attempt = 1; attempt <= maxRetries; attempt++)
            {
                _logger.LogError($"Tentativa '{attempt}' de criar conexão com o RabbitMQ");
                try
                {
                    await _rabbitMqConnection.InitializeAsync();
                    break;
                }
                catch (Exception)
                {
                    if (attempt == maxRetries)
                    {
                        _logger.LogError("Excedeu número máximo de tentativas para conectar ao RabbitMQ.");
                        throw;
                    }
                    await Task.Delay(TimeSpan.FromSeconds(delaySeconds));
                    delaySeconds += 2;
                }
            }
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }

}
