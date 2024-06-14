using Poc.Contract.Command.Notification.Events;

namespace Poc.Contract.Command.Notification.Interfaces;

public interface ITwilioProducer
{
    void PublishAsync(NotificationTwilioEvent evento);
}
