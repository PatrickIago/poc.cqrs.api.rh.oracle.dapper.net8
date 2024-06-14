using Ardalis.Result;
using MediatR;
using Microsoft.Extensions.Logging;
using Poc.Contract.Command.Notification.Events;
using Poc.Contract.Command.Notification.Interfaces;
using Poc.Contract.Command.Notification.Request;

namespace Poc.Command.Notification;

public class CreateNotificationSendGridEmailCommandHandler : IRequestHandler<CreateNotificationSendGridEmailCommand, Result>
{
    private readonly ILogger<CreateNotificationSendGridEmailCommandHandler> _logger;
    private readonly ISendGridEmailProducer _sendGridEmailProducer;
    public CreateNotificationSendGridEmailCommandHandler(ILogger<CreateNotificationSendGridEmailCommandHandler> logger,
                                                         ISendGridEmailProducer sendGridEmailProducer)
    {
        _logger = logger;
        _sendGridEmailProducer = sendGridEmailProducer;
    }
    public async Task<Result> Handle(CreateNotificationSendGridEmailCommand request, CancellationToken cancellationToken)
    {
        _sendGridEmailProducer.PublishAsync(new NotificationEmailEvent(request.From, request.Subject, request.HtmlContent, request.To, request.Name));
        return Result.SuccessWithMessage("Mensagem enviada com sucesso!");
    }
}
