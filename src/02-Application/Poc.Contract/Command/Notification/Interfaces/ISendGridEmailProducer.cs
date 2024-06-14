using Poc.Contract.Command.Notification.Events;

namespace Poc.Contract.Command.Notification.Interfaces;

public interface ISendGridEmailProducer
{
    void PublishAsync(NotificationEmailEvent evento);
}
