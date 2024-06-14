using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using poc.core.api.net8.Interface;
using Poc.Auth.TwilioWhatsApp;
using Poc.Contract.Command.Notification.Events;
using Poc.Contract.Command.Notification.Interfaces;
using System.Text;

using System.Text.Json;

namespace Poc.RabbiMQ.Producer.Notification;

public class SendGridEmailProducer : ISendGridEmailProducer
{
    private readonly IMessageBusService _messageBusService;
    private readonly IConfiguration _configuration;
    private readonly ILogger<SendGridEmailProducer> _logger;
    public SendGridEmailProducer(IMessageBusService messageBusService, IConfiguration configuration, ILogger<SendGridEmailProducer> logger)
    {
        _messageBusService = messageBusService;
        _configuration = configuration;
        _logger = logger;
    }

    public void PublishAsync(NotificationEmailEvent notification)
    {
        var notificationInfoJson = JsonSerializer.Serialize(notification);
        var notificationInfoBytes = Encoding.UTF8.GetBytes(notificationInfoJson);
        _messageBusService.Publish(_configuration.GetValue<string>(RabbiMQConsts.QUEUE_SENDGRID), notificationInfoBytes);
    }
}
