namespace Poc.Contract.Command.Notification.DTO;

public class SendGridEmailDTO
{
    public int Id { get; set; }
    public string From { get; set; }
    public string Subject { get; set; }
    public string HtmlContent { get; set; }
    public string To { get; set; }
    public string Name { get; set; }
}
