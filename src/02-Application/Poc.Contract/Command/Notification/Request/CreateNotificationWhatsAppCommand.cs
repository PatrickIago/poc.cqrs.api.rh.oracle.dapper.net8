using Ardalis.Result;
using MediatR;
using poc.core.api.net8.Enumerado;

namespace Poc.Contract.Command.Notification.Request;
public class CreateNotificationWhatsAppCommand : IRequest<Result>
{
    public ENotificationType NotificationType { get; set; }

    public int Id { get; set; }
    public string From { get; set; }
    public string Body { get; set; }
    public string To { get; set; }
}