using Poc.Contract.Command.Notification.DTO;

namespace Poc.Contract.Command.Notification.Interfaces;

public interface INotificationService
{
    Task<SendGridEmailDTO> SendGridEmail(SendGridEmailDTO dto);
    Task<TwilioWhatsAppDTO> TwilioWhatsApp(TwilioWhatsAppDTO dto);
}
