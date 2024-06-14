using Poc.Auth.SendGridEmail.Request;

namespace Poc.Auth.SendGridEmail.Interfaces;

public interface ISendGridEmailService
{
    Task SendEmailAsync(SendGridRequest dto);
}
