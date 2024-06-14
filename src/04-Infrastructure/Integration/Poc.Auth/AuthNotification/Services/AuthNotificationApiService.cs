using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Poc.Auth.AuthNotification.Interfaces;
using Poc.Auth.AuthNotification.Request;
using Poc.Auth.AuthNotification.Response;
using Poc.Auth.TwilioWhatsApp;
using System.Text;
using System.Text.Json;

namespace Poc.Auth.AuthNotification.Services;

public class AuthNotificationApiService : IAuthNotificationApiService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<AuthNotificationApiService> _logger;
    private readonly IConfiguration _configuration;

    public AuthNotificationApiService(HttpClient httpClient, ILogger<AuthNotificationApiService> logger, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _logger = logger;
        _configuration = configuration;
    }

    public async Task<string> GetTokenTwilioAsync()
    {
        var jsonRequest = JsonSerializer.Serialize(new AuthNotificationRequest
        {
            Email = _configuration.GetValue<string>(TwilioAppAuthConsts.EMAIL),
            Password = _configuration.GetValue<string>(TwilioAppAuthConsts.PASSWORD)
        });
        var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync(_configuration.GetValue<string>(TwilioAppAuthConsts.URL_AUTH), content);

        if (response.IsSuccessStatusCode)
        {
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var tokenObject = JsonSerializer.Deserialize<TokenAuthNotificationResponse>(jsonResponse);
            return tokenObject.Token;
        }
        return null;
    }

    public async Task<string> GetTokenSendGridEmailAsync()
    {
        var jsonRequest = JsonSerializer.Serialize(new AuthNotificationRequest
        {
            Email = _configuration.GetValue<string>(SendGridEmailAppAuthConsts.EMAIL),
            Password = _configuration.GetValue<string>(SendGridEmailAppAuthConsts.PASSWORD)
        });
        var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync(_configuration.GetValue<string>(SendGridEmailAppAuthConsts.URL_AUTH), content);

        if (response.IsSuccessStatusCode)
        {
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var tokenObject = JsonSerializer.Deserialize<TokenAuthNotificationResponse>(jsonResponse);
            return tokenObject.Token;
        }
        return null;
    }
}


