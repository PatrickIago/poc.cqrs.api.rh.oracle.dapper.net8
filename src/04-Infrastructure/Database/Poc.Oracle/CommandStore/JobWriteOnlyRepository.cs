using AutoMapper;
using Dapper;
using Poc.Contract.Command.Job.Interfaces;
using Poc.Contract.Query.Job.ViewModels;
using Poc.Domain.Entities.Job;
using Poc.Oracle.Context;
using Poc.Oracle.SQL;
using System.Data;

namespace Poc.Oracle.CommandStore;
public class JobWriteOnlyRepository : IJobWriteOnlyRepository
{
    private readonly OracleDbContext _dbContext;
    private readonly IMapper _mapper;

    public JobWriteOnlyRepository(OracleDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<JobQueryModel> Create(JobEntity job)
    {
        using IDbConnection dbConnection = _dbContext.CreateConnection();
        dbConnection.Open();

        var parameters = new DynamicParameters();
        parameters.Add("PR_JOB_ID", job.JobId, DbType.String);
        parameters.Add("PR_JOB_TITLE", job.JobTitle, DbType.String);
        parameters.Add("PR_MIN_SALARY", job.MinSalary, DbType.Decimal);
        parameters.Add("PR_MAX_SALARY", job.MaxSalary, DbType.Decimal);

        await dbConnection.ExecuteAsync(JobSqlConsts.SQL_INSERT, parameters);

        return _mapper.Map<JobQueryModel>(job);
    }

    public async Task<bool> Delete(string id)
    {
        using IDbConnection dbConnection = _dbContext.CreateConnection();
        dbConnection.Open();

        var parameters = new DynamicParameters();
        parameters.Add("PR_JOB_ID", id, DbType.String);

        var affectedRows = await dbConnection.ExecuteAsync(JobSqlConsts.SQL_DELETE, parameters);

        return affectedRows > 0;
    }

    public async Task<JobEntity> Get(string id)
    {
        using IDbConnection dbConnection = _dbContext.CreateConnection();
        dbConnection.Open();

        var result = await dbConnection.QueryFirstOrDefaultAsync<JobEntity>(JobSqlConsts.SQL_GET_BY_ID, new { PR_JOB_ID = id });

        return result;
    }

    public async Task<bool> Update(JobEntity job)
    {
        using IDbConnection dbConnection = _dbContext.CreateConnection();
        dbConnection.Open();

        var parameters = new DynamicParameters();
        parameters.Add("PR_JOB_ID", job.JobId, DbType.String);
        parameters.Add("PR_JOB_TITLE", job.JobTitle, DbType.String);
        parameters.Add("PR_MIN_SALARY", job.MinSalary, DbType.Decimal);
        parameters.Add("PR_MAX_SALARY", job.MaxSalary, DbType.Decimal);

        var affectedRows = await dbConnection.ExecuteAsync(JobSqlConsts.SQL_UPDATE, parameters);

        return affectedRows > 0;
    }
}