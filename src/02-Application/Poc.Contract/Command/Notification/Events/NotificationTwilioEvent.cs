using poc.core.api.net8.Enumerado;
using poc.core.api.net8.Events;

namespace Poc.Contract.Command.Notification.Events;

public class NotificationTwilioEvent : Event
{
    public NotificationTwilioEvent(ENotificationType notificationType, string from, string body, string to)
    {
        NotificationType = notificationType;
        From = from;
        Body = body;
        To = to;
    }

    public int Id { get; private set; }
    public ENotificationType NotificationType { get; private set; }
    public string From { get; private set; }
    public string Body { get; private set; }
    public string To { get; private set; }
}
