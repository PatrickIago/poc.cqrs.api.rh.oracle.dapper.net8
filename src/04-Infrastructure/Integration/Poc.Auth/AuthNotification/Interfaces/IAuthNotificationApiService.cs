namespace Poc.Auth.AuthNotification.Interfaces;

public interface IAuthNotificationApiService
{
    Task<string> GetTokenSendGridEmailAsync();
    Task<string> GetTokenTwilioAsync();
}