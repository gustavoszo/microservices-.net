using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RestauranteService.Configuration;
using System.Text;

namespace ItemService.Consumers
{
    public class ItemConsumer : BackgroundService
    {
        private readonly RabbitMqConnection _rabbitMQ;
        private readonly ILogger _logger;
        private IConfiguration _configuration;
        private string _queue;

        public ItemConsumer(RabbitMqConnection rabbit, ILogger logger, IConfiguration configuration)
        {
            _rabbitMQ = rabbit;
            _logger = logger;
            _configuration = configuration;
            _queue = _configuration["broker.queue.item.name"];
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _rabbitMQ.InitializeAsync();

            var consumer = new AsyncEventingBasicConsumer(_rabbitMQ.Channel);

            // Esse é o evento assíncrono que será chamado sempre que uma nova mensagem chegar da fila.
            consumer.ReceivedAsync += async (model, ea) =>
            {
                try
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);

                    _logger.LogInformation($"Mensagem recebida: {message}");
                    ProcessMessage(message);

                    // Confirma o recebimento da mensagem; Mensagem recebida e processada com sucesso. Pode remover da fila!"
                    await _rabbitMQ.Channel.BasicAckAsync(ea.DeliveryTag, multiple: false);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Erro ao processar a mensagem.");

                    // Deu erro ao processar a mensagem. Quero devolvê-la para a fila para tentar de novo.
                    await _rabbitMQ.Channel.BasicNackAsync(ea.DeliveryTag, false, requeue: true);
                }
            };

            // Esse método liga o consumidor na fila. Ele começa a escutar a fila por novas mensagens e dispara o ReceivedAsync automaticamente.
            await _rabbitMQ.Channel.BasicConsumeAsync(
                queue: _queue,
                autoAck: false, // Eu quem decido quando a mensagem foi processada com sucesso. Isso dá controle total com Ack ou Nack.
                consumer: consumer
            );

            // Mantém o serviço rodando enquanto não for cancelado
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(1000, stoppingToken);
            }
        }

        private void ProcessMessage(string message)
        {

        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            await base.StopAsync(cancellationToken);
        }
    }
}
