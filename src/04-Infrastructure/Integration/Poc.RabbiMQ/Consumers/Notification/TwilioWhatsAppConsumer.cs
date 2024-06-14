using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Poc.Auth.Twilio.Interfaces;
using Poc.Auth.Twilio.Request;
using Poc.Auth.TwilioWhatsApp;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace Poc.RabbiMQ.Consumers.Notification;

public class TwilioWhatsAppConsumer : BackgroundService
{
    private readonly IConnection _connection;
    private readonly IConfiguration _configuration;
    private readonly IModel _channel;
    private readonly IServiceProvider _serviceProvider;

    public TwilioWhatsAppConsumer(IServiceProvider servicesProvider, IConfiguration configuration)
    {
        _serviceProvider = servicesProvider;
        _configuration = configuration;

        var factory = new ConnectionFactory
        {
            HostName = _configuration.GetValue<string>(RabbiMQConsts.Hostname),
            Port = Convert.ToInt32(_configuration.GetValue<string>(RabbiMQConsts.Port)),
            UserName = _configuration.GetValue<string>(RabbiMQConsts.Username),
            Password = _configuration.GetValue<string>(RabbiMQConsts.Password)
        };

        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();

        _channel.QueueDeclare(
            queue: _configuration.GetValue<string>(RabbiMQConsts.QUEUE_TWILIO_WHATSAPP),
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += async (sender, eventArgs) =>
        {
            var infoBytes = eventArgs.Body.ToArray();
            var infoJson = Encoding.UTF8.GetString(infoBytes);
            var info = JsonSerializer.Deserialize<TwilioRequest>(infoJson);
            await Twilio(info);
            _channel.BasicAck(eventArgs.DeliveryTag, false);
        };
        _channel.BasicConsume(_configuration.GetValue<string>(RabbiMQConsts.QUEUE_TWILIO_WHATSAPP), false, consumer);
        return Task.CompletedTask;
    }

    public async Task Twilio(TwilioRequest dto)
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            var sendService = scope.ServiceProvider.GetRequiredService<ITwilioService>();
            await sendService.TwilioAsync(dto);
        }
    }
}