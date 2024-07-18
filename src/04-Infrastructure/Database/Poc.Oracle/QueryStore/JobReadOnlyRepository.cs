using AutoMapper;
using Dapper;
using Poc.Contract.Query.Job.Interfaces;
using Poc.Contract.Query.Job.ViewModels;
using Poc.Domain.Entities.Job;
using Poc.Oracle.Context;
using Poc.Oracle.SQL;
using System.Data;

namespace Poc.Oracle.QueryStore;
public class JobReadOnlyRepository : IJobReadOnlyRepository
{
    private readonly OracleDbContext _dbContext;
    private readonly IMapper _mapper;

    public JobReadOnlyRepository(OracleDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<List<JobQueryModel>> Get()
    {
        using IDbConnection dbConnection = _dbContext.CreateConnection();
        dbConnection.Open();

        var result = await dbConnection.QueryAsync<JobEntity>(JobSqlConsts.SQL_GET);
        var mapper = _mapper.Map<List<JobQueryModel>>(result);
        return mapper.AsList();
    }

    public async Task<JobQueryModel> Get(string id)
    {
        using IDbConnection dbConnection = _dbContext.CreateConnection();
        dbConnection.Open();

        var result = await dbConnection.QueryFirstOrDefaultAsync<JobEntity>(JobSqlConsts.SQL_GET_BY_ID, new { PR_JOB_ID = id });

        if (result == null)
        {
            return null;
        }

        var mapper = _mapper.Map<JobQueryModel>(result);
        return mapper;
    }
}
