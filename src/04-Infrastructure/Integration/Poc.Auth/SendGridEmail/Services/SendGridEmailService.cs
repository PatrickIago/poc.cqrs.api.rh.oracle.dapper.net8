using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Poc.Auth.AuthNotification.Interfaces;
using Poc.Auth.SendGridEmail.Interfaces;
using Poc.Auth.SendGridEmail.Request;
using Poc.Auth.TwilioWhatsApp;
using Polly;
using System.Net;
using System.Net.Http.Json;

namespace Poc.Auth.SendGridEmail.Services;

public class SendGridEmailService : ISendGridEmailService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<SendGridEmailService> _logger;
    private readonly IConfiguration _configuration;
    private readonly AsyncPolicy<HttpResponseMessage> _retryPolicy;
    private readonly IAuthNotificationApiService _apiService;

    public SendGridEmailService(HttpClient httpClient, ILogger<SendGridEmailService> logger, IConfiguration configuration, IAuthNotificationApiService apiService)
    {
        _httpClient = httpClient;
        _logger = logger;
        _configuration = configuration;
        _apiService = apiService;

        // Configuração da política de tentativas de retry
        _retryPolicy = Policy
            .Handle<HttpRequestException>()
            .OrResult<HttpResponseMessage>(r => !r.IsSuccessStatusCode)
            .WaitAndRetryAsync(
                retryCount: _configuration.GetValue<int>(SendGridEmailAppAuthConsts.RETRYCOUNT),
                sleepDurationProvider: retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                onRetry: (ex, retryCount, context) =>
                {
                    // Lógica a ser executada a cada tentativa de retry
                    _logger.LogWarning($"Tentativa {retryCount} de envio de e-mail...");
                }
            );
    }

    public async Task SendEmailAsync(SendGridRequest request)
    {
        var token = await _apiService.GetTokenSendGridEmailAsync();
        if (token is not null)
            await _retryPolicy.ExecuteAsync(async () =>
            {
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token.ToString());
                var response = await _httpClient.PostAsJsonAsync(
                    _configuration.GetValue<string>(SendGridEmailAppAuthConsts.URL_CREATE),
                    request
                );
                response.EnsureSuccessStatusCode();

                if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.Accepted)
                {
                    var error = await response.Content.ReadAsStringAsync();
                    _logger.LogError($"Falha ao enviar e-mail: {error}");
                }
                return response;
            });
    }
}
