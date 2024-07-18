using Ardalis.Result;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Poc.Contract.Query.Departament.Request;
using Poc.Contract.Query.Departament.Validators;
using Poc.Contract.Query.Departament.ViewModels;
using Poc.Contract.Query.Employee.Request;
using Poc.Contract.Query.Employee.Validators;
using Poc.Contract.Query.Employee.ViewModels;
using Poc.Contract.Query.Job.Request;
using Poc.Contract.Query.Job.Validators;
using Poc.Contract.Query.Job.ViewModels;
using Poc.Contract.Query.JobHistory.Request;
using Poc.Contract.Query.JobHistory.Validators;
using Poc.Contract.Query.JobHistory.ViewModels;
using Poc.Contract.Query.Region.Request;
using Poc.Contract.Query.Region.Validators;
using Poc.Contract.Query.Region.ViewModels;
using Poc.Query.Departament;
using Poc.Query.Employee;
using Poc.Query.Job;
using Poc.Query.JobHistory;
using Poc.Query.Region;

namespace Poc.Query;


public class QueryInitializer
{
    // Método estático para inicializar serviços
    public static void Initialize(IServiceCollection services)
    {
        #region Oracle
        services.AddTransient<IRequestHandler<GetRegionQuery, Result<List<RegionQueryModel>>>, GetRegionQueryHandler>();
        services.AddTransient<IRequestHandler<GetRegionByIdQuery, Result<RegionQueryModel>>, GetRegionByIdQueryHandler>();
        services.AddTransient<GetRegionByIdQueryValidator>();

        //DEPARTAMENT

        services.AddTransient<IRequestHandler<GetDepartmentQuery, Result<List<DepartmentQueryModel>>>, GetDepartmentQueryHandler>();
        services.AddTransient<IRequestHandler<GetDepartmentByIdQuery, Result<DepartmentQueryModel>>, GetDepartmentByIdQueryHandler>();
        services.AddTransient<GetDepartmentByIdQueryValidator>();

        //EMPLOYEE
        // Registra um handler para a consulta GetEmployeeQuery e mapeia para GetEmployeeQueryHandler
        services.AddTransient<IRequestHandler<GetEmployeeQuery, Result<List<EmployeeQueryModel>>>, GetEmployeeQueryHandler>();

        // Registra um handler para a consulta GetEmployeeByIdQuery e mapeia para GetEmployeeByIdQueryHandler
        services.AddTransient<IRequestHandler<GetEmployeeByIdQuery, Result<EmployeeQueryModel>>, GetEmployeeByIdQueryHandler>();

        // Registra o handler para a consulta GetEmployeeByIdQuery
        services.AddTransient<GetEmployeeByIdQueryValidator>();

        // JOBHISTORY
        services.AddTransient<IRequestHandler<GetJobHistoryQuery, Result<List<JobHistoryQueryModel>>>, GetJobHistoryQueryHandler>();
        services.AddTransient<IRequestHandler<GetJobHistoryByIdQuery, Result<JobHistoryQueryModel>>, GetJobHistoryByIdQueryHandler>();
        services.AddTransient<GetJobHistoryByIdQueryValidator>();

        // JOBH
        services.AddTransient<IRequestHandler<GetJobQuery, Result<List<JobQueryModel>>>, GetJobQueryHandler>();
        services.AddTransient<IRequestHandler<GetJobByIdQuery, Result<JobQueryModel>>, GetJobByIdQueryHandler>();
        services.AddTransient<GetJobByIdQueryValidator>();

        #endregion
    }
}