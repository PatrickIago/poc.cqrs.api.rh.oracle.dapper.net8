using Microsoft.Extensions.Configuration;
using poc.core.api.net8.Interface;
using Poc.Auth.TwilioWhatsApp;
using RabbitMQ.Client;

namespace Poc.RabbiMQ.MessageBus;

public class MessageBusService : IMessageBusService
{
    private readonly ConnectionFactory _connectionFactory;
    private readonly IConfiguration _configuration;

    public MessageBusService(IConfiguration configuration)
    {
        _configuration = configuration;
        _connectionFactory = new ConnectionFactory
        {
            HostName = _configuration.GetValue<string>(RabbiMQConsts.Hostname),
            Port = Convert.ToInt32(_configuration.GetValue<string>(RabbiMQConsts.Port)),
            UserName = _configuration.GetValue<string>(RabbiMQConsts.Username),
            Password = _configuration.GetValue<string>(RabbiMQConsts.Password)
        };
    }

    public void Publish(string queue, byte[] message)
    {
        using (var connection = _connectionFactory.CreateConnection())
        {
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(
                    queue: queue,
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);

                channel.BasicPublish(
                    exchange: "",
                    routingKey: queue,
                    basicProperties: null,
                    body: message);
            }
        }
    }
}
