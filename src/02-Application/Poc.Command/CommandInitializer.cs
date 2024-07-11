using Ardalis.Result;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Poc.Command.Departament;
using Poc.Command.Departament.Events;
using Poc.Command.Employee;
using Poc.Command.Employee.Events;
using Poc.Command.Notification;
using Poc.Command.Region;
using Poc.Command.Region.Events;
using Poc.Contract.Command.Departament.Request;
using Poc.Contract.Command.Departament.Response;
using Poc.Contract.Command.Departament.Validators;
using Poc.Contract.Command.Employee.Request;
using Poc.Contract.Command.Employee.Response;
using Poc.Contract.Command.Employee.Validators;
using Poc.Contract.Command.Notification.Request;
using Poc.Contract.Command.Region.Request;
using Poc.Contract.Command.Region.Response;
using Poc.Contract.Command.Region.Validators;
using Poc.Domain.Entities.Department.Events;
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

        // EMPLOYEE
        // Registra um handler transiente para a criação de empregado e mapeia para CreateEmployeeCommandHandler
        services.AddTransient<IRequestHandler<CreateEmployeeCommand, Result<CreateEmployeeResponse>>, CreateEmployeeCommandHandler>();
        // Registra o validador para a criação de empregado
        services.AddTransient<CreateEmployeeCommandValidator>();
        // Registra um handler de notificação para o evento de criação de empregado
        services.AddTransient<INotificationHandler<EmployeeCreatedEvent>, EmployeeCreatedEventHandler>();

        // Registra um handler transiente para a atualização de empregado e mapeia para UpdateEmployeeCommandHandler
        services.AddTransient<IRequestHandler<UpdateEmployeeCommand, Result>, UpdateEmployeeCommandHandler>();
        // Registra o validador para a atualização de empregado
        services.AddTransient<UpdateEmployeeCommandValidator>();
        // Registra um handler de notificação para o evento de atualização de empregado
        services.AddTransient<INotificationHandler<EmployeeUpdatedEvent>, EmployeeUpdateEventHandler>();

        // Registra um handler transiente para a exclusão de empregado e mapeia para DeleteEmployeeCommandHandler
        services.AddTransient<IRequestHandler<DeleteEmployeeCommand, Result>, DeleteEmployeeCommandHandler>();
        // Registra um handler de notificação para o evento de exclusão de empregado
        services.AddTransient<INotificationHandler<EmployeeDeletedEvent>, EmployeeDeleteEventHandler>();
        // Registra o validador para a exclusão de empregado
        services.AddTransient<DeleteEmployeeCommandValidator>();

        //DEPARTAMENT
        services.AddTransient<IRequestHandler<CreateDepartmentCommand, Result<CreateDepartmentResponse>>, CreateDepartmentCommandHandler>();
        services.AddTransient<CreateDepartmentCommandValidator>();
        services.AddTransient<INotificationHandler<DepartmentCreatedEvent>, DepartmentCreateEventHandler>();

        services.AddTransient<IRequestHandler<UpdateDepartmentCommand, Result>, UpdateDepartmentCommandHandler>();
        services.AddTransient<UpdateDepartmentCommandValidator>();
        services.AddTransient<INotificationHandler<DepartmentUpdatedEvent>, DepartmentUpdateEventHandler>();

        services.AddTransient<IRequestHandler<DeleteDepartmentCommand, Result>, DeleteDepartmentCommandHandler>();
        services.AddTransient<INotificationHandler<DepartmentDeletedEvent>, DepartmentDeleteEventHandler>();
        services.AddTransient<DeleteDepartmentCommandValidator>();



        #endregion
    }
}
