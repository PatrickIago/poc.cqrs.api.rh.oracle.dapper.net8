using Ardalis.Result;
using MediatR;
using poc.core.api.net8.Enumerado;

namespace Poc.Contract.Command.Notification.Request;
public class CreateNotificationSendGridEmailCommand : IRequest<Result>
{
    public ENotificationType NotificationType { get; set; }

    public string From { get; set; }
    public string Subject { get; set; }
    public string HtmlContent { get; set; }
    public string To { get; set; }
    public string Name { get; set; }
}