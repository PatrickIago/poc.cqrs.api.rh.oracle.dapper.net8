namespace Poc.Contract.Command.Notification.DTO;

public class TwilioWhatsAppDTO
{
    public int Id { get; set; }
    public string From { get; set; }
    public string Body { get; set; }
    public string To { get; set; }
}
