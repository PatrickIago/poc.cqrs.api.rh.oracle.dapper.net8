using AutoMapper;
using Dapper;
using Poc.Contract.Query.JobHistory.Interfaces;
using Poc.Contract.Query.JobHistory.ViewModels;
using Poc.Domain.Entities.JobHistory;
using Poc.Oracle.Context;
using Poc.Oracle.SQL;
using System.Data;
namespace Poc.Oracle.QueryStore;
public class JobHistoryReadOnlyRepository : IJobHistoryReadOnlyRepository
{
    private readonly OracleDbContext _dbContext;
    private readonly IMapper _mapper;

    public JobHistoryReadOnlyRepository(OracleDbContext context, IMapper mapper)
    {
        _dbContext = context;
        _mapper = mapper;
    }

    public async Task<List<JobHistoryQueryModel>> Get()
    {
        using IDbConnection dbConnection = _dbContext.CreateConnection();
        dbConnection.Open();

        var result = await dbConnection.QueryAsync<JobHistoryEntity>(JobHistorySqlConsts.SQL_GET);
        var mapper = _mapper.Map<List<JobHistoryQueryModel>>(result);
        return mapper.AsList();
    }

    public async Task<JobHistoryQueryModel> Get(decimal id)
    {
        using IDbConnection dbConnection = _dbContext.CreateConnection();
        dbConnection.Open();

        var result = await dbConnection.QueryFirstOrDefaultAsync<JobHistoryEntity>
            (JobHistorySqlConsts.SQL_GET_BY_ID, new { PR_EMPLOYEE_ID = id });

        if (result == null)
        {
            return null;
        }

        var mapper = _mapper.Map<JobHistoryQueryModel>(result);
        return mapper;
    }
}

