using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Poc.Auth.SendGridEmail.Interfaces;
using Poc.Auth.SendGridEmail.Request;
using Poc.Auth.TwilioWhatsApp;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace Poc.RabbiMQ.Consumers.Notification;

public class SendGridEmailConsumer : BackgroundService
{
    private readonly IConnection _connection;
    private readonly IConfiguration _configuration;
    private readonly IModel _channel;
    private readonly IServiceProvider _serviceProvider;

    public SendGridEmailConsumer(IServiceProvider servicesProvider, IConfiguration configuration)
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
            queue: _configuration.GetValue<string>(RabbiMQConsts.QUEUE_SENDGRID),
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
            var info = JsonSerializer.Deserialize<SendGridRequest>(infoJson);
            await SendGrid(info);
            _channel.BasicAck(eventArgs.DeliveryTag, false);
        };
        _channel.BasicConsume(_configuration.GetValue<string>(RabbiMQConsts.QUEUE_SENDGRID), false, consumer);
        return Task.CompletedTask;
    }

    public async Task SendGrid(SendGridRequest dto)
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            var sendService = scope.ServiceProvider.GetRequiredService<ISendGridEmailService>();
            await sendService.SendEmailAsync(dto);
        }
    }
}