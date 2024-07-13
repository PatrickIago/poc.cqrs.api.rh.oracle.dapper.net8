using AutoMapper;
using Dapper;
using Poc.Contract.Command.JobHistory.Interfaces;
using Poc.Contract.Query.JobHistory.ViewModels;
using Poc.Domain.Entities.JobHistory;
using Poc.Oracle.Context;
using Poc.Oracle.SQL;
using System.Data;

namespace Poc.Oracle.CommandStore;
public class JobHistoryWriteOnlyRepository : IJobHistoryWriteOnlyRepository
{
    private readonly OracleDbContext _dbContext;
    private readonly IMapper _mapper;

    public JobHistoryWriteOnlyRepository(OracleDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<JobHistoryQueryModel> Create(JobHistoryEntity jobHistory)
    {
        using IDbConnection dbConnection = _dbContext.CreateConnection();
        dbConnection.Open();

        var parameters = new DynamicParameters();
        parameters.Add("PR_EMPLOYEE_ID", jobHistory.EmployeeId, DbType.Decimal);
        parameters.Add("PR_START_DATE", jobHistory.StartDate, DbType.DateTime);
        parameters.Add("PR_END_DATE", jobHistory.EndDate, DbType.DateTime);
        parameters.Add("PR_JOB_ID", jobHistory.JobId, DbType.String);
        parameters.Add("PR_DEPARTMENT_ID", jobHistory.DepartmentId, DbType.Int32);

        await dbConnection.ExecuteAsync(JobHistorySqlConsts.SQL_INSERT, parameters);

        return _mapper.Map<JobHistoryQueryModel>(jobHistory);
    }

    public async Task<bool> Delete(decimal employeeId)
    {
        using IDbConnection dbConnection = _dbContext.CreateConnection();
        dbConnection.Open();

        var parameters = new DynamicParameters();
        parameters.Add("PR_EMPLOYEE_ID", employeeId, DbType.Decimal);

        var affectedRows = await dbConnection.ExecuteAsync(JobHistorySqlConsts.SQL_DELETE, parameters);

        return affectedRows > 0;
    }

    public async Task<JobHistoryEntity> Get(decimal id)
    {
        using IDbConnection dbConnection = _dbContext.CreateConnection();
        dbConnection.Open();

        var result = await dbConnection.QueryFirstOrDefaultAsync<JobHistoryEntity>(JobHistorySqlConsts.SQL_GET_BY_ID, new { PR_EMPLOYEE_ID = id });

        return result;
    }

    public async Task<List<JobHistoryQueryModel>> GetByEmployeeId(decimal employeeId)
    {
        using IDbConnection dbConnection = _dbContext.CreateConnection();
        dbConnection.Open();

        var parameters = new DynamicParameters();
        parameters.Add("PR_EMPLOYEE_ID", employeeId, DbType.Decimal);

        var result = await dbConnection.QueryAsync<JobHistoryEntity>(JobHistorySqlConsts.SQL_GET_BY_ID, parameters);
        var mapper = _mapper.Map<List<JobHistoryQueryModel>>(result);
        return mapper.AsList();
    }

    public async Task<bool> Update(JobHistoryEntity jobHistory)
    {
        using IDbConnection dbConnection = _dbContext.CreateConnection();
        dbConnection.Open();

        var parameters = new DynamicParameters();
        parameters.Add("PR_EMPLOYEE_ID", jobHistory.EmployeeId, DbType.Decimal);
        parameters.Add("PR_START_DATE", jobHistory.StartDate, DbType.DateTime);
        parameters.Add("PR_END_DATE", jobHistory.EndDate, DbType.DateTime);
        parameters.Add("PR_JOB_ID", jobHistory.JobId, DbType.String);
        parameters.Add("PR_DEPARTMENT_ID", jobHistory.DepartmentId, DbType.Int32);

        var affectedRows = await dbConnection.ExecuteAsync(JobHistorySqlConsts.SQL_UPDATE, parameters);

        return affectedRows > 0;
    }

}