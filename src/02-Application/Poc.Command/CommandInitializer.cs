using Ardalis.Result;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Poc.Command.Notification;
using Poc.Command.Region;
using Poc.Command.Region.Events;
using Poc.Contract.Command.Notification.Request;
using Poc.Contract.Command.Region.Request;
using Poc.Contract.Command.Region.Response;
using Poc.Contract.Command.Region.Validators;
using Poc.Domain.Entities.Region.Events;

namespace Poc.Command;

public class CommandInitializer
{
    public static void Initialize(IServiceCollection services)
    {
        #region Notificações
        services.AddTransient<IRequestHandler<CreateNotificationSMSCommand, Result>, CreateNotificationSMSCommandHandler>();
        services.AddTransient<IRequestHandler<CreateNotificationWhatsAppCommand, Result>, CreateNotificationWhatsAppCommandHandler>();
        services.AddTransient<IRequestHandler<CreateNotificationSendGridEmailCommand, Result>, CreateNotificationSendGridEmailCommandHandler>();
        #endregion

        #region Oracle
        services.AddTransient<IRequestHandler<CreateRegionCommand, Result<CreateRegionResponse>>, CreateRegionCommandHandler>();
        services.AddTransient<CreateRegionCommandValidator>();
        services.AddTransient<INotificationHandler<RegionCreatedEvent>, RegionCreatedEventHandler>();

        services.AddTransient<IRequestHandler<UpdateRegionCommand, Result>, UpdateRegionCommandHandler>();
        services.AddTransient<UpdateRegionCommandValidator>();
        services.AddTransient<INotificationHandler<RegionUpdateEvent>, RegionUpdateEventHandler>();

        services.AddTransient<IRequestHandler<DeleteRegionCommand, Result>, DeleteRegionCommandHandler>();
        services.AddTransient<INotificationHandler<RegionDeletedEvent>, RegionDeleteEventHandler>();
        services.AddTransient<DeleteRegionCommandValidator>();
        #endregion
    }
}
