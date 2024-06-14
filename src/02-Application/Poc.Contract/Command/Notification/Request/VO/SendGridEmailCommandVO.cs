namespace Poc.Contract.Command.Notification.Request.VO;

public class SendGridEmailCommandVO
{
    public string From { get; set; }
    public string Subject { get; set; }
    public string HtmlContent { get; set; }
    public string To { get; set; }
    public string Name { get; set; }
}
