using Microsoft.Extensions.DependencyInjection;
using poc.core.api.net8.Interface;
using Poc.Contract.Command.Notification.Interfaces;
using Poc.RabbiMQ.Consumers.Notification;
using Poc.RabbiMQ.MessageBus;
using Poc.RabbiMQ.Producer.Notification;

namespace Poc.RabbiMQ;

public class RabbiMQInitializer
{
    public static void Initialize(IServiceCollection services)
    {
        services.AddHttpClient();
        services.AddScoped<IMessageBusService, MessageBusService>();

        // Publish
        services.AddScoped<ISendGridEmailProducer, SendGridEmailProducer>();
        services.AddScoped<ITwilioProducer, TwilioProducer>();

        // Subscribe
        services.AddHostedService<SendGridEmailConsumer>();
        services.AddHostedService<TwilioSMSConsumer>();
        services.AddHostedService<TwilioWhatsAppConsumer>();
    }
}
