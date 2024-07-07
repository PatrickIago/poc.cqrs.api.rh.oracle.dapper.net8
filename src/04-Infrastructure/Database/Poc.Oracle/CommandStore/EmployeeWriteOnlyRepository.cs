using AutoMapper;
using Dapper;
using Poc.Contract.Command.Employee.Interfaces;
using Poc.Contract.Query.Employee.ViewModels;
using Poc.Domain.Entities.Employee;
using Poc.Oracle.Context;
using Poc.Oracle.SQL;
using System.Data;

namespace Poc.Oracle.CommandStore;
public class EmployeeWriteOnlyRepository : IEmployeeWriteOnlyRepository
{
    private readonly OracleDbContext _dbContext;
    private readonly IMapper _mapper;

    public EmployeeWriteOnlyRepository(OracleDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<EmployeeQueryModel> Create(EmployeeEntity employee)
    {
        using IDbConnection dbConnection = _dbContext.CreateConnection();
        dbConnection.Open();

        var employeeId = await dbConnection.QueryFirstOrDefaultAsync<int>(EmployeeSqlConsts.SQL_MAX);
        var parameters = new DynamicParameters();
        parameters.Add("PR_EMPLOYEE_ID", employee.Id, DbType.Int32);
        parameters.Add("PR_NAME", employee.Name, DbType.String);
        parameters.Add("PR_EMAIL", employee.Email, DbType.String);
        parameters.Add("PR_PHONE_NUMBER", employee.PhoneNumber, DbType.String);
        parameters.Add("PR_HIRE_DATE", employee.HireDate, DbType.Date);
        parameters.Add("PR_JOB_ID", employee.JobId, DbType.String);
        parameters.Add("PR_SALARY", employee.Salary, DbType.Decimal);
        parameters.Add("PR_MANAGER_ID", employee.ManagerId, DbType.Int32);
        parameters.Add("PR_DEPARTMENT", employee.Department.Id, DbType.Int32);

        await dbConnection.ExecuteAsync(EmployeeSqlConsts.SQL_INSERT, parameters);
        employee.SetEmployeeId(employeeId);
        return _mapper.Map<EmployeeQueryModel>(employee); 
    }

    public async Task<bool> Delete(int id)
    {
        using IDbConnection dbConnection = _dbContext.CreateConnection();
        dbConnection.Open();

        var parameters = new DynamicParameters();
        parameters.Add("PR_EMPLOYEE_ID", id, DbType.Int32);

        var affectedRows = await dbConnection.ExecuteAsync(EmployeeSqlConsts.SQL_DELETE, parameters);

        return affectedRows > 0; // Retorna verdadeiro se a operação deletou uma linha
    }

    public async Task<EmployeeEntity> Get(int id)
    {
        using IDbConnection dbConnection = _dbContext.CreateConnection();
        dbConnection.Open();

        var result = await dbConnection.QueryFirstOrDefaultAsync<EmployeeEntity>(
            EmployeeSqlConsts.SQL_GET_BY_ID,
            new { PR_EMPLOYEE_ID = id }
        );
        return result;
    }

    public async Task<bool> Update(EmployeeEntity employee)
    {
        using IDbConnection dbConnection = _dbContext.CreateConnection();
        dbConnection.Open();

        var parameters = new DynamicParameters();
        parameters.Add("PR_EMPLOYEE_ID", employee.Id, DbType.Int32);
        parameters.Add("PR_NAME", employee.Name, DbType.String);
        parameters.Add("PR_EMAIL", employee.Email, DbType.String);
        parameters.Add("PR_PHONE_NUMBER", employee.PhoneNumber, DbType.String);
        parameters.Add("PR_HIRE_DATE", employee.HireDate, DbType.Date);
        parameters.Add("PR_JOB_ID", employee.JobId, DbType.String);
        parameters.Add("PR_SALARY", employee.Salary, DbType.Decimal);
        parameters.Add("PR_MANAGER_ID", employee.ManagerId, DbType.Int32);
        parameters.Add("PR_DEPARTMENT", employee.Department.Id, DbType.Int32);

        var affectedRows = await dbConnection.ExecuteAsync(EmployeeSqlConsts.SQL_UPDATE, parameters);

        return affectedRows > 0; // Retorna verdadeiro se a operação atualizou uma linha
    }
}
