using poc.core.api.net8.Events;

namespace Poc.Contract.Command.Notification.Events;

public class NotificationEmailEvent : Event
{
    public NotificationEmailEvent()
    {

    }
    public NotificationEmailEvent(string from, string subject, string htmlContent, string to, string name)
    {
        From = from;
        Subject = subject;
        HtmlContent = htmlContent;
        To = to;
        Name = name;
    }

    public string From { get; private set; }
    public string Subject { get; private set; }
    public string HtmlContent { get; private set; }
    public string To { get; private set; }
    public string Name { get; private set; }
}
