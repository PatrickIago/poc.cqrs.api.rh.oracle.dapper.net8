using Microsoft.Extensions.DependencyInjection;
using Poc.Auth.AuthNotification.Interfaces;
using Poc.Auth.AuthNotification.Services;
using Poc.Auth.SendGridEmail.Interfaces;
using Poc.Auth.SendGridEmail.Services;
using Poc.Auth.Twilio.Interfaces;
using Poc.Auth.Twilio.Services;

namespace Poc.Auth;

public class IntegrationApisInitializer
{
    public static void Initialize(IServiceCollection services)
    {
        services.AddHttpClient();
        services.AddScoped<ISendGridEmailService, SendGridEmailService>();
        services.AddScoped<ITwilioService, TwilioService>();
        services.AddScoped<IAuthNotificationApiService, AuthNotificationApiService>();
    }
}

