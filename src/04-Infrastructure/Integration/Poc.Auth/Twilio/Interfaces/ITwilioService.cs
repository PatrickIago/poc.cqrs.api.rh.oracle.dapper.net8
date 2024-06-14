using Poc.Auth.Twilio.Request;

namespace Poc.Auth.Twilio.Interfaces;

public interface ITwilioService
{
    Task TwilioAsync(TwilioRequest dto);
}
