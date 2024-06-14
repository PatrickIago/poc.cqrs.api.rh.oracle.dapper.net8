using System.Text.Json.Serialization;

namespace Poc.Auth.AuthNotification.Response;

public class TokenAuthNotificationResponse
{
    [JsonPropertyName("token")]
    public string Token { get; set; }
}
