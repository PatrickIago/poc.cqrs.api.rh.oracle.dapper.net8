namespace Poc.Contract.Command.Notification.Request.VO;

public class TwilioWhatsAppCommandVO
{
    public int Id { get; set; }
    public string From { get; set; }
    public string Body { get; set; }
    public string To { get; set; }
}
