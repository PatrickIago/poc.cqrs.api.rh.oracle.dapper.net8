using Ardalis.Result;
using MediatR;
using Microsoft.Extensions.Logging;
using Poc.Contract.Command.Notification.Events;
using Poc.Contract.Command.Notification.Interfaces;
using Poc.Contract.Command.Notification.Request;
namespace Poc.Command.Notification;

public class CreateNotificationSMSCommandHandler : IRequestHandler<CreateNotificationSMSCommand, Result>
{

    private readonly ILogger<CreateNotificationSMSCommandHandler> _logger;
    private readonly ITwilioProducer _twilioProducer;
    public CreateNotificationSMSCommandHandler(ILogger<CreateNotificationSMSCommandHandler> logger,
                                            ITwilioProducer twilioProducer)
    {
        _logger = logger;
        _twilioProducer = twilioProducer;
    }
    public async Task<Result> Handle(CreateNotificationSMSCommand request, CancellationToken cancellationToken)
    {
        _twilioProducer.PublishAsync(new NotificationTwilioEvent(request.NotificationType, request.From, request.Body, request.To));
        return Result.SuccessWithMessage("Mensagem enviada com sucesso!");
    }
}
